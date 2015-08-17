using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.Presentation.Views;
using Conway.Web.UI.WebControls;

namespace Chucksoft.Templates.Default
{
    public partial class GalleryLandingView : ViewBase
    {
        private Gallery gallery;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int galleryId;

            if (int.TryParse(Request.QueryString["lgid"], out galleryId))
            {
                gallery = new GalleryLogic().RetrieveGalleryByGalleryId(galleryId);
                //GetRandomPhotoFromGallery();

                galleryDescription.Text = gallery.Description;
                galleryTitle.Text = gallery.Name;

                //Set the three random photos
                GetRandomPhotoFromGallery(firstImageLiteral, firstImagePanel);
                GetRandomPhotoFromGallery(secondImageLiteral, secondImagePanel);
                GetRandomPhotoFromGallery(thirdImageLiteral, thirdImagePanel);

                //Generate breadcrumbnavigation
                BreadCrumbs breadCrumbNavigation = GenerateBreadCrumbNavigation();
                BuildNavigation(breadCrumbNavigation);
            }
        }

        /// <summary>
        /// Builds the navigation.
        /// </summary>
        /// <param name="breadCrumbNavigation">The bread crumb navigation.</param>
        private void BuildNavigation(BreadCrumbs breadCrumbNavigation)
        {
            //Add HomeLink
            HyperLink home = new HyperLink { Text = "Galleries", NavigateUrl = "Default.aspx" };
            breadCrumbNavigation.Links.Add(home);

            Literal galleryLink = new Literal { Text = gallery.Name };
            breadCrumbNavigation.Links.Add(galleryLink);
        }

        /// <summary>
        /// Gets the random photo from gallery.
        /// </summary>
        /// <param name="imageLiteral">The image literal.</param>
        /// <param name="imagePanel">The image panel.</param>
        private void GetRandomPhotoFromGallery(ITextControl imageLiteral, WebControl imagePanel)
        {
            //retrieve random photo by gallery
            Photo randomPhoto = new PhotoLogic().RetrieveRandomPhotoByGalleryId(gallery.GalleryId);
            const string imageLinkFormat = "<a href=\"?gid={0}\" title=\"{1}\" ><img title=\"{1}\" alt=\"{1}\" src=\"GalleryImage.ashx?t={2}\" /></a>";

            //Get handle on literal
            imageLiteral.Text = string.Format(imageLinkFormat, gallery.GalleryId, gallery.Name, randomPhoto.PhotoID);
            imagePanel.CssClass = (randomPhoto.Profile == PhotoProfile.Landscape ? "landscapelandingimage" : "landscapelandingimage");
        }
    }
}