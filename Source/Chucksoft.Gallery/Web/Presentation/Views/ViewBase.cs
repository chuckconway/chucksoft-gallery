using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Conway.Web.UI.WebControls;


namespace Chucksoft.Web.Presentation.Views
{
    public class ViewBase : UserControl
    {
        /// <summary>
        /// Find BreadCrumbs in Page Control collection
        /// </summary>
        /// <returns></returns>
        public BreadCrumbs GenerateBreadCrumbNavigation()
        {
            BreadCrumbs breadCrumbNavigation = FindControlRecursive(Page.Master, "galleryBreadCrumbs") as BreadCrumbs;
            return breadCrumbNavigation;
        }

        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Control FindControlRecursive(Control root, string id)
        {
            //if it's teh root element return it...
            if (root.ID == id)
            {
                return root;
            }

            //Recursivly walk all control collections
            foreach (Control ctl in root.Controls)
            {
                Control foundCtl = FindControlRecursive(ctl, id);
                if (foundCtl != null)
                {
                    return foundCtl;
                }
            }

            //nothing found return nullage
            return null;
        }
    }
}
