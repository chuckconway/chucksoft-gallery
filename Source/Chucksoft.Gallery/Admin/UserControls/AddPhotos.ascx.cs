using System;
using Chucksoft.Web.Controls.UserControls;

namespace Chucksoft.Admin.UserControls
{
    public partial class AddPhoto : UserControlBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Add New Photos - PhotoGallery - By Chucksoft";
        }

    }
}