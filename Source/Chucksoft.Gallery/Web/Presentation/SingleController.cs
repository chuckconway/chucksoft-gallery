using System.Web.UI;
using Chucksoft.Entities;

namespace Chucksoft.Web.Presentation
{
    public class SingleController : ControllerBase
    {
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <value>The view.</value>
        public override PresentationMode View
        {
            get { return PresentationMode.Single; }
        }

        /// <summary>
        /// Gets the view from the factory
        /// </summary>
        /// <returns></returns>
        public override Control GenerateView()
        {
            Control control = new Control();
            return control;
        }

    }
}
