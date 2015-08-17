using System.Web.UI;
using Chucksoft.Entities;

namespace Chucksoft.Web.Presentation
{
    /// <summary>
    /// Base class for the Presentation Factory
    /// </summary>
    public abstract class ControllerBase
    {
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <value>The view.</value>
        public abstract PresentationMode View { get; }

        /// <summary>
        /// Gets the view from the factory
        /// </summary>
        /// <returns></returns>
        public abstract Control GenerateView();


        /// <summary>
        /// Loads the contorl bases on type.
        /// </summary>
        /// <typeparam name="T">Type of control to be loaded.</typeparam>
        /// <returns></returns>
        protected static Control GetControl<T>() where T : UserControl, new()
        {
            GallerySettings settings = GallerySettings.Load();

            T view = new T();
            view = (T)view.LoadControl("Templates\\" + settings.Theme + "\\" + view.GetType().Name + ".ascx");

            return view;
        }
    }
}
