using System;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Conway.Web.UI;
using Chucksoft.Web.Controls.UserControls;


namespace Chucksoft.Admin.UserControls
{
    public partial class AddGallery : UserControlBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
           // GallerySettings settings = GallerySettings.Load();
        }

        /// <summary>
        /// Handles the Click event of the addGallery control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void addGallery_Click(object sender, EventArgs e)
        {
           Gallery gallery = new Gallery
                                           {
                                               Name = galleryName.Text.Trim(),

                                               UserId = CurrentUser.UserId,
                                               Description = description.Text.Trim()
                                           };

           new GalleryLogic().Insert(gallery);

            //display message
            message.Text = string.Format(AdminResources.SuccessfulGalleryAdd, gallery.Name);
            
            //Clears the textboxes
            WebControlUtilities.ClearTextFromControl<TextBox>(Controls);
        }
    }
}