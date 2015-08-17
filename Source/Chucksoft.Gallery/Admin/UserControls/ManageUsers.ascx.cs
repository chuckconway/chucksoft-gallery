using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Controls.UserControls;

namespace Chucksoft.Admin.UserControls
{
    public partial class ManageUsers : UserControlBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Manage Users - PhotoGallery - By Chucksoft";
            
            if(!IsPostBack)
            {
                BindData();  
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            manageUsers.DataSource = new UserLogic().RetrieveAllUsers();
            manageUsers.DataBind();
        }

        /// <summary>
        /// Handles the RowEditing event of the manageUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void manageUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grid = (GridView)sender;
            int userId = Int32.Parse(grid.DataKeys[e.NewEditIndex].Value.ToString());

            Server.Transfer("~/Admin/Default.aspx?a=Users&uid=" + userId, false);
        }

        /// <summary>
        /// Retrieves the selected rows.
        /// </summary>
        /// <returns></returns>
        private List<User> RetrieveSelectedRows()
        {
            List<GridViewRow> selectedRows = RetrieveCheckedRowsFromGridview("selectedRow", manageUsers);
            List<User> selectedPhotos = new List<User>();

            foreach (GridViewRow row in selectedRows)
            {
                User user = new User { UserId = Int32.Parse(manageUsers.DataKeys[row.RowIndex].Value.ToString()) };

                selectedPhotos.Add(user);
            }

            return selectedPhotos;
        }

        /// <summary>
        /// Handles the Click event of the deleteGalleriesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void deleteGalleriesButton_Click(object sender, EventArgs e)
        {
            //Retrieve selected rows and delete the selected users.
            List<User> users = RetrieveSelectedRows();
            new UserLogic().Delete(users);

            //Rebind the data
            BindData();
        }

    }
}