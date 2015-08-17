using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Controls.UserControls;

namespace Chucksoft.Admin.UserControls
{
    public partial class ManagePhotos : UserControlBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Manage Photos - PhotoGallery - By Chucksoft";

            if(!IsPostBack)
            {
                BindGalleryDropDownData();
                BindToGallery();
                BindPhotoGridView();
            }
        }

        /// <summary>
        /// set the gallery retrieved from the querystring
        /// </summary>
        private void BindToGallery()
        {
            //If nothing is found, move out of the method
            if (!string.IsNullOrEmpty(Request.QueryString["g"]))
            {
                string galleryID = Request.QueryString["g"];
                ListItem item = galleriesDropDownList.Items.FindByValue(galleryID);

                //set the item as selected
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// Handles the RowUpdating event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grid = (GridView)sender;

            GridViewRow row = grid.Rows[e.RowIndex];
            int photoID = Int32.Parse(grid.DataKeys[e.RowIndex].Value.ToString());

            TextBox title = (TextBox)row.FindControl("titleTextBox");
            TextBox description = (TextBox)row.FindControl("descriptionTextBox");
            TextBox date = (TextBox)row.FindControl("photoDate");

            Photo photo = new Photo {PhotoID = photoID, Title = title.Text, Description = description.Text, DateTaken = DateTime.Parse(date.Text)};

            new PhotoLogic().UpdateMetaData(photo);

            //Reset the edit index.
            managePhotos.EditIndex = -1;

            //Bind data to the GridView control.
            BindPhotoGridView();
        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Set the edit index.
            managePhotos.EditIndex = -1;
            BindPhotoGridView();
        }

        /// <summary>
        /// Handles the RowEditing event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            managePhotos.EditIndex = e.NewEditIndex;
            BindPhotoGridView(managePhotos.PageIndex);
        }

        /// <summary>
        /// Handles the RowDeleting event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Photo> photos = new List<Photo>();

            for (int index = 0; managePhotos.Rows.Count > index; index++)
            {
                CheckBox deleteCheckBox = (CheckBox)managePhotos.Rows[index].FindControl("selectedCheckBox");
                Photo photo = new Photo();

                if (deleteCheckBox.Checked)
                {
                    photo.PhotoID = Convert.ToInt32(managePhotos.DataKeys[index].Value);
                    photos.Add(photo);
                }
            }

            new PhotoLogic().Delete(photos);
            message.Text = AdminResources.DeletePhotos;

            BindPhotoGridView();
        }

        /// <summary>
        /// Bind galleries to the gallery dropdowns on the page.
        /// </summary>
        private void BindGalleryDropDownData()
        {
            //populate the move dropdownlist.
            PopulateGalleryDropDown(moveGalleriesDropDown);
            
            //Populate the dropdown list
            PopulateGalleryDropDown(galleriesDropDownList);

            //Add default to show all images.
            galleriesDropDownList.Items.Insert(0, new ListItem("All","0"));
        }

        /// <summary>
        /// Populates the gallery drop downs on this page
        /// </summary>
        /// <param name="dropdown">Dropdownlist Reference</param>
        private void PopulateGalleryDropDown(ListControl dropdown)
        {
            //Need to Cache this...
            List<Gallery> galleries = new GalleryLogic().RetrieveGalleriesByUserId(CurrentUser.UserId);
            
            dropdown.DataSource = galleries;
            dropdown.DataTextField = "Name";
            dropdown.DataValueField = "GalleryID";
            dropdown.DataBind();
        }

        /// <summary>
        /// Binds the photo grid view.
        /// </summary>
        private void BindPhotoGridView()
        {
            BindPhotoGridView(0);
        }

        /// <summary>
        /// Binds the photo grid view.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindPhotoGridView(int pageIndex)
        {
            int selectedIndex = Convert.ToInt32(galleriesDropDownList.SelectedValue);
            List<Photo> photos = selectedIndex == 0 ? new  PhotoLogic().RetrievePhotosByUserID(CurrentUser.UserId) : new PhotoLogic().RetrievePhotosByUserIDAndGalleryId(CurrentUser.UserId, selectedIndex);

            managePhotos.DataSource = photos;
            managePhotos.PageIndex = pageIndex;
            managePhotos.DataBind();

        }

        /// <summary>
        /// Handles the RowDataBound event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Photo photo = (Photo)e.Row.DataItem;
                const string imageLinkFormat = "<a href=\"AdminImage.ashx?f={0}\" title=\"{1}\" rel=\"lightbox[{2}]\" ><img title=\"{2}\" alt=\"{2}\" src=\"AdminImage.ashx?t={0}\" /></a>";

                Literal imageLink = (Literal)e.Row.FindControl("imageLiteral");

                //editLink.NavigateUrl = "../Default.aspx?pid=" + photo.PhotoID;
                imageLink.Text = string.Format(imageLinkFormat, photo.PhotoID, photo.Title, "10");
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the galleriesDropDownList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void galleriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPhotoGridView();
        }

        /// <summary>
        /// Shows the Move Panel or Delete Panel, by what RadioButton is checked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void movePhotos_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = (RadioButton)sender;

            //if the radiobutton has the texted of "Move" then we are going to show the move panel, otherwise we show the delete panel
            bool isMovePanel = selectedButton.Text.Equals("Move", StringComparison.CurrentCultureIgnoreCase);

            //Set the visabilty based on what radiobutton is selected.
            movePhotosPanel.Visible = isMovePanel;
            deletePhotosPanel.Visible = !isMovePanel;
        }

        /// <summary>
        /// Handles the Click event of the MovePhotosLinkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void MovePhotosLinkButton_Click(object sender, EventArgs e)
        {
            //Get Selected Rows
            List<Photo> photos = RetrieveSelectedRows();
            new PhotoLogic().MovePhotoToNewGallery(photos);

            //Rebind with remaining photos
            BindPhotoGridView();
        }

        /// <summary>
        /// Retrieves the selected rows.
        /// </summary>
        /// <returns></returns>
        private List<Photo> RetrieveSelectedRows()
        {
            List<GridViewRow> selectedRows = RetrieveCheckedRowsFromGridview("selectedRow", managePhotos);
            List<Photo> selectedPhotos = new List<Photo>();

            foreach (GridViewRow row in selectedRows)
            {
                Photo photo = new Photo {PhotoID = Int32.Parse(managePhotos.DataKeys[row.RowIndex].Value.ToString()), GalleryId = Convert.ToInt32(moveGalleriesDropDown.SelectedValue)};

                selectedPhotos.Add(photo);
            }

            return selectedPhotos;
        }

        /// <summary>
        /// Handles the Click event of the deleteSelectedPhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void deleteSelectedPhotos_Click(object sender, EventArgs e)
        {
            //Get Selected Rows
            List<Photo> photos = RetrieveSelectedRows();

            //Deleted Selected Photos.
            new PhotoLogic().Delete(photos);

            //Rebind with remaining photos
            BindPhotoGridView();
        }

        /// <summary>
        /// Handles the PageIndexChanging event of the managePhotos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void managePhotos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Bind data to new page.
            BindPhotoGridView(e.NewPageIndex);
        }
    }
}