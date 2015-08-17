using System;
using System.Web.UI;
using Chucksoft.Entities;
using Chucksoft.Logic;

namespace Chucksoft.Web.Presentation.Views
{
    public partial class SingleView : ViewBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve settings and set the site title.
            GallerySettings settings = GallerySettings.Load();
            Page.Title = settings.GalleryTitle;

            Photo photo = new PhotoLogic().RetrieveLastestPhoto();

            //If no title, then don't render title control
            if (!string.IsNullOrEmpty(photo.Title))
            {
                imageTitle.Text = photo.Title;
                imageTitle.Visible = true;
            }

            //populate the WebForm controls.
            image.ImageUrl = "GalleryImage.ashx?f=" + photo.PhotoID;
            image.AlternateText = photo.Title;

            description.Text = photo.Description;
        }
        
    }
}