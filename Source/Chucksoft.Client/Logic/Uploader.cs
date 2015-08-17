using System.Collections.Generic;
using System.ComponentModel;
using Chucksoft.Client.Entities;
using Chucksoft.Client.PhotoService;
using System.ServiceModel;

namespace Chucksoft.Client.Logic
{
    // A delegate type for hooking up change notifications.
    public delegate void FileUploadCompletedEventHandler(object sender, GenericEventArgs<string> e);


    /// <summary>
    /// Uploads all the images
    /// </summary>
    public class Uploader
    {
        readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly List<ClientImage> _images;

        /// <summary>
        /// Occurs when [file upload complete].
        /// </summary>
        public event FileUploadCompletedEventHandler FileUploadComplete;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnFileUploadComplete(GenericEventArgs<string> e)
        {
            if (FileUploadComplete != null)
            {
                FileUploadComplete(this,e);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Uploader"/> class.
        /// </summary>
        /// <param name="images">The images.</param>
        public Uploader(List<ClientImage> images)
        {
            _images = images;

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string completedImage = e.UserState.ToString();
            GenericEventArgs<string> args = new GenericEventArgs<string>(completedImage);
            OnFileUploadComplete(args);
        }

        /// <summary>
        /// Uploads this instance.
        /// </summary>
        public void Upload()
        {
            worker.RunWorkerAsync(_images);
        }

        /// <summary>
        /// Handles the DoWork event of the worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //PhotoServiceSoapClient service = new PhotoServiceSoapClient();
            List<ClientImage> images = (List<ClientImage>)e.Argument;

            foreach (ClientImage image in images)
            {
                PhotoServiceSoapClient service = new PhotoServiceSoapClient();

                service.Upload(Client.Default.token, image.PhotoBytes, image.GalleryId, ".jpg");
                //worker.ReportProgress(1, image.ImageName);

                if (service.State == CommunicationState.Closed || service.State == CommunicationState.Closing)
                {
                    service.Close();
                }
            }
        }
    }
}
