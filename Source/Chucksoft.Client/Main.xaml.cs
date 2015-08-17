using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Chucksoft.Client.Entities;
using Chucksoft.Client.GalleryService;
using Chucksoft.Client.Logic;
using Chucksoft.Client.PhotoService;
using Chucksoft.Client.UserControls;
using Microsoft.Win32;
using System.ServiceModel;

namespace Chucksoft.Client
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {

        private Gallery[] galleries;
        private int completedCount;
        private double increment;
        private double percentageRatio;

        public Main()
        {
            InitializeComponent();

            ResetProgressBar();

        }

        /// <summary>
        /// Resets the progress bar.
        /// </summary>
        private void ResetProgressBar()
        {
            uploadStatus.Width = 0;
            progressText.Text = "0%";
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
           // PhotoServiceSoapClient service = new PhotoServiceSoapClient();

            //check for selected gallery, if none selected prompt the user to select a gallery.
            if (galleryListBox.SelectedItem != null && uploadListBox.Items.Count > 0)
            {
                List<ClientImage> images = new List<ClientImage>();

                foreach (object image in uploadListBox.Items)
                {
                    ClientImage clientImage = (ClientImage) image;
                    images.Add(clientImage);
                }


                Uploader upload = new Uploader(images);
                upload.FileUploadComplete += new FileUploadCompletedEventHandler(upload_FileUploadComplete);
                upload.Upload();

                //CalculateProgressbar();

                //foreach (object image in uploadListBox.Items)
                //{
                //    ClientImage clientImage = (ClientImage)image;
                //    images.Add(clientImage);

                //    //string extension = Path.GetExtension(clientImage.ImageName);

                //    ////Upload the photos
                //    //service.Upload(Client.Default.token, clientImage.PhotoBytes, clientImage.GalleryId, extension);
                //    //UploadComplete();
                //}
            }
            else
            {
                //if there is no gallery selected then show the gallery error message
                //otherwise the problem must be no photos in the upload listbox
                uploadErrorMessage.Text = (galleryListBox.SelectedItem == null ? "Please select a gallery." : "Please choose a photo to upload.");
            }
        }

        /// <summary>
        /// Handles the FileUploadComplete event of the upload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="string"/> instance containing the event data.</param>
        void upload_FileUploadComplete(object sender, GenericEventArgs<string> e)
        {
            //Updates the progressbar
            UploadComplete();
        }


        /// <summary>
        /// Uploads the complete.
        /// </summary>
        private void UploadComplete()
        {
            //increment the completed file
            completedCount++;

            //Get the width
            double currentWidth = (completedCount * increment);

            //find the current upload percentage
            progressText.Text = Convert.ToString((int)(currentWidth * percentageRatio)) + "%";

            //take into account rounding errors.
            if (currentWidth > uploadStatusBackground.Width || (completedCount + 1) >= uploadListBox.Items.Count)
            {
                currentWidth = uploadStatusBackground.Width;
                progressText.Text = "100%";
            }

            //Finally set the width.
            uploadStatus.Width = currentWidth;
        }

        /// <summary>
        /// Calculates the progressbar.
        /// </summary>
        private void CalculateProgressbar()
        {
            uploadStatus.Width = 0;

            // ReSharper disable PossibleLossOfFraction
            increment = (int)uploadStatusBackground.Width / uploadListBox.Items.Count;
            // ReSharper restore PossibleLossOfFraction

            percentageRatio = 100 / uploadStatusBackground.Width;
        }

        /// <summary>
        /// Gets the byte array from stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] GetByteArrayFromStream(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];

            using (stream)
            {
                stream.Read(bytes, 0, (int)stream.Length);
            }

            return bytes;
        }

        /// <summary>
        /// Handles the Click event of the clearAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            //Clears all Items
            uploadListBox.Items.Clear();
        }

        /// <summary>
        /// Handles the Click event of the removeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            //remove at selected index
            if(uploadListBox.SelectedIndex != -1)
            {
                uploadListBox.Items.RemoveAt(uploadListBox.SelectedIndex);  
            }
        }

        private void galleryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="files">The files.</param>
        private void AddItems(OpenFileDialog files)
        {
            Stream[] fileStreams = files.OpenFiles();
            for (int index = 0; index < files.FileNames.Length; index++)
            {
                Gallery gallery = ((Gallery)galleryListBox.SelectedItem);

                ClientImage image = new ClientImage
                                        {
                                            ImageName = TruncatePath(files.FileNames[index]),
                                            GalleryName = gallery.Name,
                                            GalleryId = gallery.GalleryID,
                                            PhotoBytes = GetByteArrayFromStream(fileStreams[index])

                                         };

                uploadListBox.Items.Add(image);
            }

        }

        /// <summary>
        /// Truncates the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static string TruncatePath(string path)
        {
            string filename = Path.GetFileName(path);
            string directory = Path.GetDirectoryName(path);
            string trucatedPath = (directory.Length > 17 ? directory.Substring(0, 17) + "...\\" : directory);
            string trucatedPathandFile = trucatedPath + filename;

            return trucatedPathandFile;
        }

        /// <summary>
        /// Handles the Click event of the addPhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void addPhotos_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog {Filter = "Images *.Jpg |*.jpg", Multiselect = true};

            bool? okClicked = fileDialog.ShowDialog();

            if(okClicked != false)
            {
                AddItems(fileDialog);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool showWindow = string.IsNullOrEmpty(Client.Default.token);

            if(showWindow)
            {
                Authentication auth = new Authentication {Owner = this};
                auth.ShowDialog();
            }

            GalleryServiceSoapClient client = new GalleryServiceSoapClient();

            //retrieve the galleries
            try
            {
                galleries = client.GetAllGalleries(Client.Default.token);
                galleryListBox.DataContext = galleries;
            }
            catch
            {
                //TODO:Logging need to add logging.
                throw;
            }
            finally
            {
                //If the state is not closed, then close it.
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }

        }
    }
}
