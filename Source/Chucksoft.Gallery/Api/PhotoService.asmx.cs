using System;
using System.ComponentModel;
using System.Web.Services;
using Chucksoft.Entities;
using Chucksoft.Entities.Exceptions;
using Chucksoft.Logic;

namespace Chucksoft.Api
{
    /// <summary>
    /// Summary description for PhotoService
    /// </summary>
    [WebService(Namespace = "http://chucksoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PhotoService : WebService
    {

        /// <summary>
        /// Uploads the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="image">The image.</param>
        /// <param name="galleryId">The gallery id.</param>
        /// <param name="imageFormat">The image format.</param>
        [WebMethod]
        public void Upload(string token, byte[] image, int galleryId, string imageFormat)
        {
            bool isTokenValid = UserLogic.IsValidToken(token);

            //Check for the validity of the token.
            if (!isTokenValid)
            {
                throw new InvalidTokenException();
            }

            //User user = UserLogic.RetrieveUserByToken(token);
            Photo photo = new Photo { GalleryId = galleryId, Title = string.Empty, DateTaken = DateTime.Now, Description = string.Empty };
            GalleryPhoto galleryphoto = new GalleryPhoto { OriginalImage = image };

            new PhotoLogic().Insert(photo, galleryphoto);
        }
    }
}
