using System.Web;
using System.Web.UI;
using Chucksoft.Entities;
using Chucksoft.Templates.Default;
using Chucksoft.Web.Presentation.Views;

namespace Chucksoft.Web.Presentation
{
    public class GalleryController : ControllerBase
    {
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <value>The view.</value>
        public override PresentationMode View
        {
            get { return PresentationMode.Gallery; }
        }

        /// <summary>
        /// Gets the view from the factory
        /// </summary>
        /// <returns></returns>
        public override Control GenerateView()
        {
            Control control;

            if(!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["gid"]))
            {
               control = GetControl<PhotoView>();
            }
            else
            {
                control = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["lgid"]) ? GetControl<GalleryLandingView>() : GetControl<GalleryView>());
            }

            return control;
        }

    }
}
