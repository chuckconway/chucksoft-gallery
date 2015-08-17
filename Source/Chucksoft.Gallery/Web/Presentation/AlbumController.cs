using System.Web;
using System.Web.UI;
using Chucksoft.Entities;
using Chucksoft.Web.Presentation.Views;

namespace Chucksoft.Web.Presentation
{
    public class AlbumController : ControllerBase
    {
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <value></value>
        public override PresentationMode View
        {
            get { return PresentationMode.Album; }
        }

        /// <summary>
        /// Gets the view from the factory
        /// </summary>
        /// <returns></returns>
        public override Control GenerateView()
        {
            Control control;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["aid"]))
            {
                control = GetControl<GalleryView>();
            }
            else
            {
                control = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["gid"]) ? GetControl<PhotoView>() : GetControl<AlbumView>());
            }

            return control;
        }
    }
}
