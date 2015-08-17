using System.Web;
using System;

namespace Chucksoft
{
    public class Global : HttpApplication
    {

        protected void Application_OnStart()
        {
            //Enable SqlCe to run under a web server
            AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
        }

        //protected void Application_Start(object sender, EventArgs e)
        //{

        //}

        //protected void Session_Start(object sender, EventArgs e)
        //{

        //}

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{

        //}

        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{

        //}

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            if (Server != null)
            {
                Exception ex = Server.GetLastError();

                if (Response.StatusCode != 404 )
                {
                    //Logging.Error("Caught in Global.asax", ex);
                }

                //Context.ClearError();
            }
     

        }

        //protected void Session_End(object sender, EventArgs e)
        //{

        //}

        //protected void Application_End(object sender, EventArgs e)
        //{

        //}
    }
}