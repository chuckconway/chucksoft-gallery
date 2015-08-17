using System.Web;

namespace Chucksoft.Web.HttpHandlers
{
    public class PhotoHandlerHelper
    {
        private GetPhoto _thumbnailMethod;

        /// <summary>
        /// Set the thumbnail method.
        /// </summary>
        /// <value>The thumbnail method.</value>
        public GetPhoto ThumbnailMethod 
        {
            set { _thumbnailMethod = value; }
        }

        private  GetPhoto _fullsizeMethod;

        /// <summary>
        /// Set the Fullsize method.
        /// </summary>
        /// <value>The fullsize method.</value>
        public GetPhoto FullsizeMethod
        {
            set { _fullsizeMethod = value; }
        }

        /// <summary>
        /// Contructs the PhotoHandlerHelper.
        /// </summary>
        /// <param name="thumbnailMethod">The method to retrieve the thumbnail</param>
        /// <param name="fullsizeMethod">The methmod to retrieve the fullsize image</param>
        public PhotoHandlerHelper(GetPhoto thumbnailMethod, GetPhoto fullsizeMethod)
        {
            _thumbnailMethod = thumbnailMethod;
            _fullsizeMethod = fullsizeMethod;
        }

        public delegate byte[] GetPhoto(int photoID);
        /// <summary>
        /// Retrieves the image.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public byte[] RetrieveImage(HttpContext context)
        {

            byte[] image = new byte[128];

            //Retrieve thumbnail from Resource
            if (!string.IsNullOrEmpty(context.Request["t"]))
            {
                image = GetImage(context.Request["t"], _thumbnailMethod);
            }
            //Retrieve fullsize image
            else if (!string.IsNullOrEmpty(context.Request["f"]))
            {
                image = GetImage(context.Request["f"], _fullsizeMethod);
            }

            return image;
        }

        /// <summary>
        /// Gets the image from the database, checks ID for integer type.
        /// </summary>
        /// <param name="photoID">string value of photoid</param>
        /// <param name="getPhotoMethod">Method to retreive photo</param>
        /// <returns>byte array of image.</returns>
        private static byte[] GetImage(string photoID, GetPhoto getPhotoMethod)
        {
            //declare image container
            byte[] image = new byte[128];

            //local id variable
            int imageId;

            //test string for type of int, if it is assign it to the imageID
            if (int.TryParse(photoID, out imageId))
            {
                //Retreive image with delegate method
                image = getPhotoMethod(imageId);
            }

            return image;
        }
    }
}
