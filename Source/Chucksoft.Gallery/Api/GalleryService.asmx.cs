using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Chucksoft.Web.ServiceModel;


namespace Chucksoft.Api
{
    /// <summary>
    /// Summary description for GalleryService1
    /// </summary>
    [WebService(Namespace = "http://chucksoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GalleryService : WebService
    {

        /// <summary>
        /// Gets all galleries.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [WebMethod]
        public List<Gallery> GetAllGalleries(string token)
        {
            ApiHelper.ValidToken(token);
            User user = new UserLogic().RetrieveUserByToken(token);
            List<Gallery> galleries = new GalleryLogic().RetrieveGalleriesByUserId(user.UserId);

            return galleries;
        }
    }
}
