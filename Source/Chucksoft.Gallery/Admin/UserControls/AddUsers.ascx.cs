using System;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Controls.UserControls;
using Conway.Security;
using Conway.Web.UI;

namespace Chucksoft.Admin.UserControls
{
    public partial class AddUser : UserControlBase
    {
        User editUser = new User();
        private bool IsUserEdit;
        int userId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            IsUserEdit = !string.IsNullOrEmpty(Request.QueryString["uid"]);
            Page.Title = "Add New User - PhotoGallery - By Chucksoft";

            //if in edit mode, retrieve user.
            if (IsUserEdit)
            {
                //Set Edit Text
                h1HeaderLiteral.Text = "Edit User";
                saveLinkButton.Text = "Save";
                Page.Title = "Edit User - PhotoGallery - By Chucksoft";

                if(int.TryParse(Request.QueryString["uid"], out userId))
                {
                    //Retrieve the user by UserId and populate fields on the page.
                   editUser = new UserLogic().RetreiveUserByUserId(userId);
                   PopulateFields();
                }
            }
        }

        /// <summary>
        /// Populates the fields.
        /// </summary>
        private void PopulateFields()
        {
            emailAddressTextBox.Text = editUser.Email;
            firstNameTextBox.Text = editUser.FirstName;
            lastNameTextBox.Text = editUser.LastName;
            websiteTextBox.Text = editUser.Website;
        }

        /// <summary>
        /// Handles the Click event of the saveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void saveButton_Click(object sender, EventArgs e)
        {

            User user = new User
                            {
                                Access = 0,
                                Email = emailAddressTextBox.Text.Trim(),
                                FirstName = firstNameTextBox.Text.Trim(),
                                LastName = lastNameTextBox.Text.Trim(),
                                Website = websiteTextBox.Text.Trim()
                            };

            bool passwordFieldsHaveValues = !string.IsNullOrEmpty(passwordOneTextBox.Text.Trim()) && !string.IsNullOrEmpty(passwordTwoTextBox.Text.Trim());
            bool isValidPassword = true;

            //Check passwords if they exist
            if (passwordFieldsHaveValues)
            {
                isValidPassword = IsValidPassword();

                if(isValidPassword)
                {
                    user.Password = SimpleHash.ComputeHash(passwordOneTextBox.Text.Trim(), SimpleHash.Algorithm.SHA256, new byte[8]);
                }
                else
                {
                    message.Text = "Passwords don't match";
                }
            }
            else
            {
                if(!IsUserEdit)
                {
                    message.Text = "New Users require a password"; 
                }
            }

            
            //Check that there is a valid password and the error text is empty
            if (isValidPassword && string.IsNullOrEmpty(message.Text))
            {

                if (IsUserEdit)
                {
                    //If no password values were entered we need to keep the old password.
                    if (!passwordFieldsHaveValues)
                    {
                        user.Password = editUser.Password;
                    }

                    //Set user Id
                    user.UserId = userId;

                    //update the user and display the success message.
                    new UserLogic().Update(user);
                    //message.Text = string.Format(AdminResources.SuccessfulUserUpdate);
                    Response.Redirect("~/Admin/Manage.aspx?a=Users", false);
                }
                else
                {
                    new UserLogic().Add(user); 
                    message.Text = string.Format(AdminResources.SuccessfulUserAdd, user.Email);
                }
                
                //Clears the textboxes
                WebControlUtilities.ClearTextFromControl<TextBox>(Controls);
               
            }
        }

        /// <summary>
        /// Determines whether [is valid password].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is valid password]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidPassword()
        {
            bool isValidPassword = string.Equals(passwordOneTextBox.Text.Trim(), passwordTwoTextBox.Text.Trim(), StringComparison.CurrentCultureIgnoreCase);
            return isValidPassword;
        }
    }
}