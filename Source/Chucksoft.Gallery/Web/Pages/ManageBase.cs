using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using System.Threading;
using Chucksoft.Web.Security;
using Chucksoft.Admin;
using Chucksoft.Logic;

namespace Chucksoft.Web.Pages
{
    public class ManageBase : SitePageBase
    {
        /// <summary>
        /// Discovers the module.
        /// </summary>
        /// <param name="queryParameter">The query parameter.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <param name="holder">The holder.</param>
        protected void DiscoverModule(string queryParameter, string pageType, PlaceHolder holder)
        {
            //look for querystring value
            if (string.IsNullOrEmpty(Request[queryParameter]))
            {
                throw new Exception("Can't find requested requested module.");
            }

            //extract the querystring value
            string addValue = Request[queryParameter];
            string virtualPath = string.Format("UserControls\\{0}{1}.ascx", pageType, addValue);
            string path = Server.MapPath(virtualPath);

            //File is missing...
            if (!File.Exists(path))
            {
                throw new Exception("Requested module '" + addValue + "' was not found.");
            }

            //Add finally load the control
            holder.Controls.Add(LoadControl(virtualPath));
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.InitComplete"/> event after page initialization.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            Authorization authorize = new Authorization();
            bool isAuthorized = authorize.AuthenticatedUser();

            if(isAuthorized)
            {
                SetAuthorizedUser(authorize);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        /// <summary>
        /// Sets the authorized user.
        /// </summary>
        /// <param name="authorize">The authorize.</param>
        private void SetAuthorizedUser(Authorization authorize)
        {
            User user = authorize.RetrieveUser();

            HttpCookie sessionCookie = new HttpCookie(AdminResources.AdminSessionCookieName);
            sessionCookie["Token"] = new UserLogic().GenerateUserToken(user.ServiceKey);
            sessionCookie["Meat"] = "Chicken";
            sessionCookie.Expires = DateTime.Now.AddMonths(1);

            Authorization.SetUnencryptedCookie(sessionCookie);
                
            CurrentUser = user;
            Thread.CurrentPrincipal = CurrentUser;
        }
    }
}