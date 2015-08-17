using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Controls.UserControls;

namespace Chucksoft.Admin.UserControls
{
    public partial class ManageGalleries : UserControlBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Manage Galleries - PhotoGallery - By Chucksoft";

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
            manageGalleries.DataSource = new GalleryLogic().RetrieveAllGalleriesByUserIdWithPhotoCount(CurrentUser.UserId);
            manageGalleries.DataBind();
        }

        /// <summary>
        /// Handles the RowDataBound event of the manageGalleries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void manageGalleries_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = (HyperLink)e.Row.FindControl("galleryEditLink");
                Gallery gallery = (Gallery)e.Row.DataItem;
                link.Text = gallery.Name;
                link.ToolTip = "Edit " + gallery.Name;

                link.NavigateUrl = ResolveUrl("~/Admin/Manage.aspx?g=" + gallery.GalleryId);
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            List<int> galleryIds = new List<int>();
            
            for(int index = 0; manageGalleries.Rows.Count > index; index++)
            {
                CheckBox deleteCheckBox = (CheckBox)manageGalleries.Rows[index].FindControl("selectedCheckBox");

                if(deleteCheckBox.Checked)
                {
                    int galleryId = Convert.ToInt32(manageGalleries.DataKeys[index].Value);
                    galleryIds.Add(galleryId);
                }
            }

            new GalleryLogic().Delete(galleryIds);
            message.Text = AdminResources.DeletedGalleries;
        }

       // GridViewPageEventArgs e

        /// <summary>
        /// Handles the RowUpdating event of the manageGalleries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void manageGalleries_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowEditing event of the manageGalleries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void manageGalleries_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the manageGalleries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void manageGalleries_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}