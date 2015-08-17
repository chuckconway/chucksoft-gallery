using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Conway.Web.UI.WebControls;

namespace Chucksoft.Web.Presentation.Views
{
    public partial class GalleryView : ViewBase
    {
        private List<Gallery> galleries;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["aid"]))
            {
                //galleries
            }
            else
            {
                galleries = new GalleryLogic().RetrieveGalleriesWithPhotos();

                //Generate breadcrumbnavigation
                BreadCrumbs breadCrumbNavigation = GenerateBreadCrumbNavigation();
                BuildNavigation(breadCrumbNavigation);
            }


            
            if(!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// Builds the navigation.
        /// </summary>
        /// <param name="breadCrumbNavigation">The bread crumb navigation.</param>
        private static void BuildNavigation(BreadCrumbs breadCrumbNavigation)
        {
            //Add HomeLink
            Literal home = new Literal { Text = "Galleries" };
            breadCrumbNavigation.Links.Add(home);
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            galleryViewRepeater.DataSource = galleries;
            galleryViewRepeater.DataBind();
        }

        /// <summary>
        /// Handles the ItemDataBound event of the galleryViewRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void galleryViewRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Gallery gallery = (Gallery)e.Item.DataItem;

                //retrieve random photo by gallery
                Photo randomPhoto = new PhotoLogic().RetrieveRandomPhotoByGalleryId(gallery.GalleryId);
                const string imageLinkFormat = "<a href=\"?lgid={0}\" title=\"{1}\" ><img title=\"{1}\" alt=\"{1}\" src=\"GalleryImage.ashx?t={2}\" /></a>";
                
                //Get handle on literal
                Literal imageHtml = (Literal)e.Item.FindControl("imageLiteral");
                imageHtml.Text = string.Format(imageLinkFormat, gallery.GalleryId, gallery.Name, randomPhoto.PhotoID);

                //Set Gallery title
                Literal galleryTitle = (Literal)e.Item.FindControl("titleLiteral");
                galleryTitle.Text = gallery.Name;

                //Set Gallery photo count
                Literal photoCount = (Literal)e.Item.FindControl("photoCount");
                photoCount.Text = gallery.PhotoCount + (gallery.PhotoCount == 1 ? " Photo" : " Photos");

            }
        }

    }
}