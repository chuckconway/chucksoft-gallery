using System.Collections.Generic;
using Chucksoft.Entities;
using System.Drawing;
using Chucksoft.Entities.Interfaces.Resources;
using Chucksoft.Resources.Database;

namespace Chucksoft.Logic
{
    public class PhotoLogic
    {
        private readonly IPhotoDb _resource;
        public PhotoLogic() : this(new PhotoDb()) {}
        

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoLogic"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public PhotoLogic(IPhotoDb resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// Inserts the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="galleryPhoto">The gallery photo.</param>
		public void Insert(Photo photo, GalleryPhoto galleryPhoto)
		{
            GalleryPhoto adminPhotos = new GalleryPhoto();
            
            GallerySettings settings = GallerySettings.Load();
            Bitmap orginalBitmap = ImageConversion.ConvertByteArrayToBitmap(galleryPhoto.OriginalImage);

            //hardcoded administration image dimensions. This will allow the user to change their image sizes and not have it effect my pretty admin layout.
            ImageDimension adminThumbnailDimensions = new ImageDimension { Height = 100, Width = 100 };
            ImageDimension adminFullsizeDimensions = new ImageDimension { Height = 600, Width = 600 };

            adminThumbnailDimensions = FindImagePerspective(orginalBitmap, adminThumbnailDimensions);
            adminFullsizeDimensions = FindImagePerspective(orginalBitmap, adminFullsizeDimensions);

            //Resize the Admin images
            adminPhotos.ThumbnailImage = ImageConversion.Resize(galleryPhoto.OriginalImage, adminThumbnailDimensions);
            adminPhotos.DisplayImage = ImageConversion.Resize(galleryPhoto.OriginalImage, adminFullsizeDimensions);

            //calculate the correct dimensions
		    ImageDimension thumbnailDimensions = FindImagePerspective(orginalBitmap, settings.ThumbnailDimensions);
            ImageDimension fullsizeDimensions = FindImagePerspective(orginalBitmap, settings.FullsizeDimensions);

            //Resize the images 
            galleryPhoto.ThumbnailImage = ImageConversion.Resize(galleryPhoto.OriginalImage, thumbnailDimensions);
            galleryPhoto.DisplayImage = ImageConversion.Resize(galleryPhoto.OriginalImage, fullsizeDimensions);

            //Set photo profile
		    photo.Profile = fullsizeDimensions.PhotoProfile;

            //Insert new images into the Database
            _resource.Insert(photo, galleryPhoto, adminPhotos);
		}

        /// <summary>
        /// Retrieves the random photo.
        /// </summary>
        /// <returns></returns>
        public  Photo RetrieveRandomPhoto()
        {
            Photo photo = _resource.RetrieveRandomPhoto();
            return photo;
        }

        /// <summary>
        /// Retrieves the random photo by gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public Photo RetrieveRandomPhotoByGalleryId(int galleryId)
        {
           Photo photo = _resource.RetrieveRandomPhotoByGalleryId(galleryId);
            return photo;
        }

        /// <summary>
        /// Retreieves all photos by gallery ID
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public  List<Photo> RetrievePhotosByGalleryId(int galleryId)
        {
            //string key = "GalleryID_" + galleryId;

            ////set the cache refresh/retreiveal method
            //RefreshCacheCall<List<Photo>> photoRetreiveMethod = 
            //delegate
            //{
            //    List<Photo> photoz = _resource.RetreivePhotosByGalleryId(galleryId);

            //    return photoz;
            //};

            //List<Photo> photos = Cache.RetrieveFromCache(key, new TimeSpan(1, 0, 0), _lock, photoRetreiveMethod);            
            //return photos;

            List<Photo> photoz = _resource.RetreivePhotosByGalleryId(galleryId);

            return photoz;
        }

        /// <summary>
        /// Finds the image perspective.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="maxImageDimension">The max image dimension.</param>
        /// <returns></returns>
        public static ImageDimension FindImagePerspective(Bitmap photo, ImageDimension maxImageDimension)
        {
            ImageDimension dimension = new ImageDimension();

            //determine if it's a portrait or landscape
            if (photo.Height > photo.Width)
            {
                //portrait
                decimal ratio = (photo.Width / (decimal)photo.Height);

                dimension.Height = (photo.Height < maxImageDimension.Height ? photo.Height : maxImageDimension.Height);
                dimension.Width = (int)(ratio * dimension.Height);

                dimension.PhotoProfile = PhotoProfile.Portrait;
            }
            else
            {
                //landscape
                decimal ratio = (photo.Height / (decimal)photo.Width);
                
                dimension.Width = (photo.Width < maxImageDimension.Width ? photo.Width : maxImageDimension.Width);
                dimension.Height = (int)(ratio * dimension.Width);

                dimension.PhotoProfile = PhotoProfile.Landscape;
               
            }

            return dimension;
        }

        /// <summary>
        /// Retrieves the photos by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserID(int userID)
        {
            List<Photo> photos = _resource.RetrievePhotosByUserID(userID);
            return photos;
        }

        /// <summary>
        /// Retrieves the photos by user ID and gallery id.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="galleryID">The gallery ID.</param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryID)
        {
            List<Photo> photos = _resource.RetrievePhotosByUserIDAndGalleryId(userID, galleryID);

            return photos;
        }

        /// <summary>
        /// Retrieves the lastest photo.
        /// </summary>
        /// <returns></returns>
        public Photo RetrieveLastestPhoto()
        {
            Photo photo = _resource.RetrieveLatestPhoto();
            return photo;
        }


        /// <summary>
        /// Retrieve the thumbnail image from the Resource layer
        /// </summary>
        /// <param name="photoId">photo unique key</param>
        /// <returns>bytes of image</returns>
        public byte[] RetrieveThumbnailImage(int photoId)
        {
            byte[] image = _resource.RetrieveThumbnailByPhotoId(photoId);
            return image;
        }


        /// <summary>
        /// Retrieve the Original Image from the Resource layer
        /// </summary>
        /// <param name="photoId">photo unique key</param>
        /// <returns>bytes of image</returns>
        public byte[] RetrieveOriginalImage(int photoId)
        {
            byte[] image = _resource.RetrieveOriginalByPhotoId(photoId);
            return image;
        }

        /// <summary>
        /// Retrieve the Display Image from the Resource layer
        /// </summary>
        /// <param name="photoId">photo unique key</param>
        /// <returns>bytes of image</returns>
        public byte[] RetrieveDisplayImage(int photoId)
        {
            byte[] image = _resource.RetrieveFullsizeByPhotoId(photoId);
            return image;
        }


 



        /// <summary>
        /// Retrieve Thumbnails for admin interface
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public byte[] RetrieveAdminThumbnail(int photoId)
        {
            byte[] image = _resource.RetrieveAdminThumbnailByPhotoId(photoId);
            return image;
        }

        /// <summary>
        /// Retrieve Fullsize Image for Admin interface
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public  byte[] RetrieveAdminImage(int photoId)
        {
            byte[] image = _resource.RetrieveAdminFullsizeByPhotoId(photoId);
            return image;
        }

        /// <summary>
        /// Moves the photo to new gallery.
        /// </summary>
        /// <param name="photos">The photos.</param>
        public void MovePhotoToNewGallery(List<Photo> photos)
        {
            _resource.MovePhotosToNewGallery(photos);
        }

        /// <summary>
        /// Updates the meta data.
        /// </summary>
        /// <param name="photo">The photo.</param>
        public void UpdateMetaData(Photo photo)
        {
            _resource.UpdateMetaData(photo);
        }

        /// <summary>
        /// Deletes the specified photos.
        /// </summary>
        /// <param name="photos">The photos.</param>
        public void Delete(List<Photo> photos)
        {
            _resource.Delete(photos);
        }
    }
}