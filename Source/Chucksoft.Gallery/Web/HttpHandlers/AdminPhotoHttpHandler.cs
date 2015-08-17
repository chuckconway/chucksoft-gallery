using System.Web;
using Chucksoft.Logic;

namespace Chucksoft.Web.HttpHandlers
{
    public class AdminPhotoHttpHandler : IHttpHandler
    {
        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            PhotoHandlerHelper helper = new PhotoHandlerHelper(new PhotoLogic().RetrieveAdminThumbnail, new PhotoLogic().RetrieveAdminImage);

            byte[] image = helper.RetrieveImage(context);

            context.Response.ContentType = "image/jpg";
            context.Response.BinaryWrite(image);
        }

        #endregion
    }
}
