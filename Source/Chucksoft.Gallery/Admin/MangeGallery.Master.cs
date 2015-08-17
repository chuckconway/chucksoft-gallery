using System;
using Chucksoft.Entities;
using System.Threading;

namespace Chucksoft.Admin
{
    public partial class MangeGallery : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = ((User)Thread.CurrentPrincipal);
            userFirstName.Text = user.FirstName;

            LoadSubNavigation();
        }

        /// <summary>
        /// Loads the sub navigation.
        /// </summary>
        private void LoadSubNavigation()
        {
            GallerySettings settings = GallerySettings.Load();

            string navigation = 
            "<ul id=\"subnavigation\">" + Environment.NewLine +
            "<li><a href=\"?a=Photos\" title=\"Add new photos\">Photos</a></li>" + Environment.NewLine +
            "<li><a href=\"?a=Galleries\" title=\"Add new galleries\">Galleries</a></li>" + Environment.NewLine +
            (settings.PresentationMode == PresentationMode.Album ? "<li><a href=\"?a=Albums\" title=\"Add new albums!\">Albums</a></li>" + Environment.NewLine : string.Empty ) +
            "<li><a href=\"?a=Users\" title=\"Add new users!\">Users</a></li>" + Environment.NewLine +
            "</ul>" + Environment.NewLine;

            subNavigation.Text = navigation;
        }
    }
}