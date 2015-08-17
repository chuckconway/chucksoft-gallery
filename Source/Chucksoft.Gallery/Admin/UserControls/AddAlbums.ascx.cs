using System;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Controls.UserControls;
using Conway.Web.UI;

namespace Chucksoft.Admin.UserControls
{
    public partial class AddAlbum : UserControlBase
    {
        /// <summary>
        /// Handles the Click event of the addAlbum control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void addAlbum_Click(object sender, EventArgs e)
        {
            Album album = new Album {Name = albumName.Text.Trim(), Description = description.Text.Trim(), UserId = CurrentUser.UserId};
            AlbumLogic.Add(album);

            message.Text = string.Format(AdminResources.SuccessfulAlbumAdd, album.Name);
            
            //Clears the textboxes
            WebControlUtilities.ClearTextFromControl<TextBox>(Controls);
        }
    }
}