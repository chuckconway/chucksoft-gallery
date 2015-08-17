using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Conway.Web.UI.WebControls;

namespace Chucksoft.Web.Presentation.Views
{
    public partial class PhotoView : ViewBase
    {
        private int galleryId;
        private Gallery gallery;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            galleryId = Convert.ToInt32(Request["gid"]);
            gallery = new GalleryLogic().RetrieveGalleryByGalleryId(galleryId);

            Page.Title = string.Format("Photos in {0} | {1}", gallery.Name, Page.Title);

            //Generate breadcrumbnavigation
            BreadCrumbs breadCrumbNavigation = GenerateBreadCrumbNavigation();
            BuildNavigation(breadCrumbNavigation);

            if(!IsPostBack)
            {
                BindData();
            }
        }


        /// <summary>
        /// Builds the navigation.
        /// </summary>
        /// <param name="breadCrumbNavigation">The bread crumb navigation.</param>
        private void BuildNavigation(BreadCrumbs breadCrumbNavigation)
        {
            //Add HomeLink
            HyperLink home = new HyperLink {Text = "Galleries", NavigateUrl = "Default.aspx"};
            breadCrumbNavigation.Links.Add(home);

            HyperLink galleryLink = new HyperLink { Text = gallery.Name, NavigateUrl = "Default.aspx?lgid=" + gallery.GalleryId };
            breadCrumbNavigation.Links.Add(galleryLink);

            Literal photos = new Literal { Text = "Photos"};
            breadCrumbNavigation.Links.Add(photos);

        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            List<Photo> photos = new PhotoLogic().RetrievePhotosByGalleryId(galleryId);

            photoRepeater.DataSource = photos;
            photoRepeater.DataBind();
        }

        /// <summary>
        /// Handles the ItemDataBound event of the photoRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void photoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Photo photo = (Photo)e.Item.DataItem;
                const string imageLinkFormat = "<a href=\"GalleryImage.ashx?f={0}\" title=\"{1}{3}\" rel=\"lightbox[{2}]\" ><img title=\"{1}{3}\" alt=\"{2}\" src=\"GalleryImage.ashx?t={0}\" /></a>";
                Literal imageHtml = (Literal)e.Item.FindControl("imageLiteral");

                string description = (string.IsNullOrEmpty(photo.Description) ? string.Empty : " - " + photo.Description);
                imageHtml.Text = string.Format(imageLinkFormat, photo.PhotoID, photo.Title, galleryId, description);
            }
        }
    }
}