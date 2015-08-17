using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using Chucksoft.Silverlight.Controls.Entities;
using Chucksoft.Silverlight.Controls.GalleryService;
using Chucksoft.Silverlight.Controls.PhotoService;
using Chucksoft.Silverlight.Controls.UI.Browser;


namespace Chucksoft.Silverlight.Controls
{
    public partial class Page
    {
        private string userToken;
        private List<Gallery> galleries;
        private int completedCount;
        private double increment;
        private double percentageRatio;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            // Required to initialize variables
            InitializeComponent();

            //reset the progress bar
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

        /// <summary>
        /// Open file selector
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //clears the error message.
            ClearErrorMessage();

            //reset progressbar
            ResetProgressBar();

            //OpenFileDialog fileSelector = new OpenFileDialog {EnableMultipleSelection = true};
            IEnumerable<FileInfo> files = new List<FileInfo>();

            OpenFileDialog fileSelector = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image files (*.jpg;*.png)|*.jpg;*.png"
            };

            bool? result = fileSelector.ShowDialog();

            if (result != null && result == true)
            {
                files = fileSelector.Files;
            }

            //Check for selected gallery
            if (galleryListBox.SelectedItem != null)
            {
                AddItems(files);
                fileCountTextBlock.Text = (uploadListBox.Items.Count == 1 ? "1 file" : uploadListBox.Items.Count + " files");
            }
            else
            {
                uploadErrorMessage.Text = "Please select a gallery.";
            }

        }

        /// <summary>
        /// Clears the error message.
        /// </summary>
        private void ClearErrorMessage()
        {
            uploadErrorMessage.Text = string.Empty;
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="files">The files.</param>
        private void AddItems(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo item in files)
            {
                Gallery gallery = ((Gallery)galleryListBox.SelectedItem);

                ClientImage image = new ClientImage
                {
                    FileInfo = item,
                    ImageName = item.Name,
                    GalleryName = gallery.Name,
                    GalleryId = gallery.GalleryId
                };

                uploadListBox.Items.Add(image);
            }
        }

        /// <summary>
        /// Load galleries after they have been retrieved from the web service.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Chucksoft.Silverlight.Controls.GalleryService.GetAllGalleriesCompletedEventArgs"/> instance containing the event data.</param>
        void galleries_GetAllGalleriesCompleted(object sender, GetAllGalleriesCompletedEventArgs e)
        {
            galleries = e.Result;

            //Check for galleries in the collection. If none exisit display a warning
            if (galleries.Count > 0)
            {
                galleryListBox.ItemsSource = galleries;
                uploadButton.IsEnabled = true;
            }
            else
            {
                uploadErrorMessage.Text = "No galleries found, please add a gallery by clicking on the galleries link above.";
                uploadButton.IsEnabled = false;
            }
        }

        //private void SetUserToken()
        //{
        //    userToken = Cookie.GetCookie("AdminSession");
        //}

        //private void gallerySlider_ValueChanged(object sender, RoutedEventArgs e)
        //{
        //    int galleryIndex = (int)(gallerySlider.Value - 1);
        //    galleryTextBox.Text = galleries[galleryIndex].Namek__BackingField;
        //    currentGalleryTextBox.Text = ((int)gallerySlider.Value).ToString();
        //}

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //userToken = "7RnTDIQeIJpM3k92N7Ya0qu+vYwmzHb2q/dE+dupBzEKRwY05xdVRq7pRGwsXlgtBZqXdb30fNkkArISu58udQ==";

            //BasicHttpBinding binding = new BasicHttpBinding {Name = "galleryBinding"};
            //EndpointAddress endpointAddress = new EndpointAddress("http://chuckconway.com/api/GalleryService.svc");

            //Load Galleries

           userToken = CookieHelper.GetCookie("Token");
            //userToken = "Vgqpi/hbzA64H5qFhky/5Fo4yGrxagl6aDRlIaSKaMqX6Bdw5moZz7aMPZbHzOcSOsHOYGt013gN7toVoE3UVw==";



            GalleryServiceSoapClient galleriesClient = new GalleryServiceSoapClient();
            galleriesClient.GetAllGalleriesCompleted += galleries_GetAllGalleriesCompleted;

            galleriesClient.GetAllGalleriesAsync(userToken);

        }

        //private void Page_Loaded(object sender, EventArgs e)
        //{

        //    // Grab SynchronizationContext while on UI Thread
        //    syncContext = SynchronizationContext.Current;



        //    // Create request
        //    HttpWebRequest request = WebRequest.Create("http://msite.com/myFeed") as HttpWebRequest;
        //    request.Method = "POST";
        //    request.Headers["x-custom-header"] = "value";


        //    // Make async call for request stream.  Callback will be called on a background thread.
        //    IAsyncResult asyncResult = request.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), request);

        //}

        //private void UploadBinaryFile(byte[] fileBits)
        //{
        //    string url = "http://chuckconway.com/gallery/photoupload.ashx";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
        //    request.ContentType = "application/octet-stream";
        //    request.Method = "POST";

        //    IAsyncResult asyncResult = request.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), request);

        //}
        

             //string chuck = "chuck";

           //  using(Stream requestStream = request.EndGetRequestStream(ar))
           //  {
           //     requestStream.Write(
           //  }

             
                
           //  StreamWriter streamWriter = new StreamWriter(requestStream);

           //  streamWriter.Write("<?xml version="1.0"?>"
           //                     + "<entry xmlns="http://www.w3.org/2005/Atom">"
           //                     + "<author>"
           //                    + "<name>Elizabeth Bennet</name>"
           //                     + "<email>liz@gmail.com</email>"
           //                     + "</author>"
           //                    + "<title type="text">Entry 1</title>"
           //                    + "<content type="text">This is my entry</content>"
           //                    + "</entry>");
                                                                               

           //  streamWriter.Close();
          

           //// Make async call for response.  Callback will be called on a background thread.
           // request.BeginGetResponse(new AsyncCallback(ResponseCallback), request);

         //}

        //private void ResponseCallback(IAsyncResult ar)
        //{

        //    HttpWebRequest request = ar.AsyncState as HttpWebRequest;
        //    WebResponse response = request.EndGetResponse(ar);

        //    // Invoke onto UI thread
        //    syncContext.Post(ExtractResponse, response);

        //    // use response.  Could include reading response stream.

        //}

        //private void ExtractResponse(object state)
        //{
        //    HttpWebResponse response = state as HttpWebResponse;
        //    // use response.  Could include reading response stream.

        //}

             //private void OpenConnection()
             //{
             //    using (SocketClient client = new SocketClient("chuckconway.com", 80))
             //    {
             //        client.
             //    }  

             //}

             //private void OnSocketConnectCompleted(object sender, SocketAsyncEventArgs e)
             //{
             //    byte[] response = new byte[1024];
             //    e.SetBuffer(response, 0, response.Length);
             //    e.Completed -= new EventHandler<SocketAsyncEventArgs>(OnSocketConnectCompleted);
             //    //e.Completed += new EventHandler<SocketAsyncEventArgs>(OnSocketReceive);
             //    Socket socket = (Socket)e.UserToken;
             //    socket.ReceiveAsync(e);
             //}

        /// <summary>
        /// Handles the Click event of the uploadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            //WebClient client = new WebClient();

            //string url = "http://chuckconway.com/gallery/photoupload.ashx";

            PhotoServiceSoapClient service = new PhotoServiceSoapClient();
            service.UploadCompleted += (service_UploadCompleted);

            //check for selected gallery, if none selected prompt the user to select a gallery.
            if (galleryListBox.SelectedItem != null && uploadListBox.Items.Count > 0)
            {
                CalculateProgressbar();

                foreach (object image in uploadListBox.Items)
                {
                    ClientImage clientImage = (ClientImage)image;

                    string extension = File.GetFileExtension(clientImage.ImageName);
                    byte[] bytes = File.GetByteArrayFromStream(clientImage.FileInfo.OpenRead());

                    //Upload the photos
                    service.UploadAsync(userToken, bytes, clientImage.GalleryId, extension);
                }
            }
            else
            {
                //if there is no gallery selected then show the gallery error message
                //otherwise the problem must be no photos in the upload listbox
                uploadErrorMessage.Text = (galleryListBox.SelectedItem == null ? "Please select a gallery." : "Please choose a photo to upload.");
            }

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
        /// Handles the UploadCompleted event of the service control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.AsyncCompletedEventArgs"/> instance containing the event data.</param>
        void service_UploadCompleted(object sender, AsyncCompletedEventArgs e)
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
        /// Handles the Click event of the removeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (uploadListBox.SelectedIndex >= 0)
            {
                //Remove item from the Listbox at selected index.
                uploadListBox.Items.RemoveAt(uploadListBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Remove all items from the ListBox
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            //Clears all items in the listBox
            uploadListBox.Items.Clear();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the galleryListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void galleryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Remove the current error message
            ClearErrorMessage();

            //Reset Progress Bar
            ResetProgressBar();
        }
    }
}
