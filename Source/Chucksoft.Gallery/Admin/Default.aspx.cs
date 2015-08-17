using System;
using Chucksoft.Web.Pages;

namespace Chucksoft.Admin
{
    public partial class Default : ManageBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["a"]))
            {
                const string queryValue = "a";
                const string type = "Add";
            
                DiscoverModule(queryValue, type, addHolder);
            }
            else
            {
                addHolder.Controls.Add(LoadControl(string.Format("UserControls\\{0}{1}.ascx", "Add", "Photos")));
            }
        }
    }
}