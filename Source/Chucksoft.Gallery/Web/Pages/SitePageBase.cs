using System;
using System.Web.UI;

namespace Chucksoft.Web.Pages
{
    public class SitePageBase :Page
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static ScriptManager GetCurrent(Page page)
        {
            //if page is null, forget it and throw exeception
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            //check page for script manager.
            ScriptManager sm = page.Items[typeof(ScriptManager)] as ScriptManager;

            //if the scriptmanager is not found, then check the MasterPage.
            if (sm == null)
            {
                //Check if masterPage exists and if there are any Child controls
                if (page.Master != null && page.Master.HasControls())
                {
                    //search the control collection for the Script manager
                    foreach (Control control in page.Master.Controls)
                    {
                        if (control.GetType() == typeof(ScriptManager))
                        {
                            sm = control as ScriptManager;
                        }
                    }
                }
            }

            //if the scriptmanger wasn't found this value will be null
            return sm;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Page.PreInit"/> event at the beginning of page initialization.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            //If ScriptManager already a part of the page.
            ScriptManager scriptManager = GetCurrent(this);

            //add the scriptmanager if it's not.
            if (scriptManager == null)
            {
                scriptManager = new ScriptManager {EnablePageMethods = true, EnablePartialRendering = true};

                //Discover location of HtmlForm (MasterPage or Page)
                Control control = (Master != null ? Master.FindControl("form1") : FindControl("form1"));

                if (control != null)
                {
                    control.Controls.AddAt(0, scriptManager);
                }
            }

            base.OnPreInit(e);
        }
    }
}