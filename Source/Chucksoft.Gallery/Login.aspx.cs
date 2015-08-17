using System;
using System.Text;
using Chucksoft.Admin;
using Chucksoft.Logic;
using Chucksoft.Web.Security;
using Conway.Security;
using System.Web.UI;

namespace Chucksoft
{
    public partial class Login : Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the loginButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void loginButton_Click(object sender, EventArgs e)
        {
            //Clean up username and hash password
            string username = emailAddressTextBox.Text.Trim();
            string password = SimpleHash.ComputeHash(passwordTextBox.Text.Trim(), SimpleHash.Algorithm.SHA256, new byte[8]);

            bool isValidUser = new UserLogic().AuthenticateUser(username, password);

            if(!isValidUser)
            {
                message.Text = AdminResources.LoginInvalidCredentials;
            }
            else
            {
                username = Convert.ToBase64String(Encoding.ASCII.GetBytes(username));
                Authorization.SetAuthenticationCookie(username, password);
                Response.Redirect(AdminResources.AdminHomepage, true);
            }
        }
    }
}