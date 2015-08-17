using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlServer
{
    public class PhotoSql : IPhoto
    {
        PhotoDB photoDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoSql"/> class.
        /// </summary>
        public PhotoSql()
        {
            photoDb = new PhotoDB();
            photoDb.DbClient = new SqlServerClient();
        }

        /// <summary>
        /// Inserts Photo into the Photos Table
        /// </summary>
        /// <param name="photo">A new populated photo.</param>
        /// <param name="galleryPhoto"></param>
        /// <param name="adminPhotos">Photos to be used in admin section</param>
        /// <returns>Insert Count</returns>
        public int Insert(Photo photo, GalleryPhoto galleryPhoto, GalleryPhoto adminPhotos)
        {
            return photoDb.Insert(photo, galleryPhoto, adminPhotos, "Photo_Insert");
        }


        /// <summary>
        /// Updates the Photo table by the primary key, if the Photo is dirty then an update will occur
        /// </summary>
        /// <param name="photo">a populated photo</param>
        /// <returns>update count</returns>
        public int UpdateMetaData(Photo photo)
        {
            int updateCount = photoDb.UpdateMetaData(photo, "Photo_UpdateMetaData");
            return updateCount;
        }

        /// <summary>
        /// update gallery images
        /// </summary>
        /// <param name="photoID">id of the photo entry</param>
        /// <param name="photos">new versions of the images</param>
        /// <returns></returns>
        public int UpdateImages(int photoID, GalleryPhoto photos)
        {
            int updateCount = photoDb.UpdateImages(photoID, photos, "Photo_ImagesUpdate");
            return updateCount;
        }

        private string deleteMethod = "Photo_Delete";

        /// <summary>
        /// Delete a Photo by the primary key
        /// </summary>
        /// <param name="photo"></param>
        public void Delete(Photo photo)
        {
            photoDb.Delete(photo, deleteMethod);
        }

        /// <summary>
        /// Deletes a collection of Photos
        /// </summary>
        /// <param name="photos">Collection photos to be deleted</param>
        public void Delete(List<Photo> photos)
        {
            photoDb.Delete(photos, deleteMethod);

        } 

        /// <summary>
        /// Retreive random photo by gallery id
        /// </summary>
        /// <param name="galleryId">Id of gallery</param>
        /// <returns></returns>
        public Photo RetrieveRandomPhotoByGalleryId(int galleryId)
        {
            Photo photo = photoDb.RetrieveRandomPhotoByGalleryId(galleryId, "Photo_RetreiveRandomPhotoByGalleryId");
            return photo;
        }

        /// <summary>
        /// Retrieves Random photo from the database
        /// </summary>
        /// <returns></returns>
        public Photo RetrieveRandomPhoto()
        {
            Photo photo = photoDb.RetrieveRandomPhoto("Photo_RetreiveRandomPhoto");
           return photo;
        }

        /// <summary>
        /// Get's thumbnail of current site image
        /// </summary>
        /// <param name="photoId">Id of row in photo table</param>
        /// <returns>byte array of image</returns>
        public byte[] RetrieveThumbnailByPhotoId(int photoId)
        {
            byte[] imageBytes = photoDb.GetImage(photoId, "Photo_RetrieveThumbnailByPhotoID");
            return imageBytes;
        }

        /// <summary>
        /// Get's site fullsize image
        /// </summary>
        /// <param name="photoId">Id of row in photo table</param>
        /// <returns>byte array of image</returns>
        public byte[] RetrieveFullsizeByPhotoId(int photoId)
        {
            byte[] imageBytes = photoDb.GetImage(photoId, "Photo_RetrieveDisplayByPhotoID");
            return imageBytes;

        }

        /// <summary>
        /// Gets thumbnail image
        /// </summary>
        /// <param name="photoId">Id of row in photo table</param>
        /// <returns>byte array of image</returns>
        public byte[] RetrieveAdminThumbnailByPhotoId(int photoId)
        {
            byte[] imageBytes = photoDb.GetImage(photoId, "Photo_RetrieveAdminThumbnailByPhotoID");
            return imageBytes;
        }

        /// <summary>
        /// Get fullsize image
        /// </summary>
        /// <param name="photoId">Id of row in photo table</param>
        /// <returns>byte array of image</returns>
        public byte[] RetrieveAdminFullsizeByPhotoId(int photoId)
        {
            byte[] imageBytes = photoDb.GetImage(photoId, "Photo_RetrieveAdminImageByPhotoID");
            return imageBytes;
        }

        private string movePhotoProcedure = "Photo_MovePhotoToNewGallery";

        /// <summary>
        /// Move a photo from one gallery to another
        /// </summary>
        /// <param name="photo">photo to be move to the new gallery</param>
        public void MovePhotoToNewGallery(Photo photo)
        {
            photoDb.MovePhotoToNewGallery(photo, movePhotoProcedure);
        }

        /// <summary>
        /// Moves a collection of photos to a new gallery
        /// </summary>
        /// <param name="photos"></param>
        public void MovePhotosToNewGallery(List<Photo> photos)
        {
            photoDb.MovePhotosToNewGallery(photos, movePhotoProcedure);

        }

        /// <summary>
        /// Retrieve all photos in a gallery
        /// </summary>
        /// <param name="galleryId">Id of the gallery</param>
        /// <returns></returns>
        public List<Photo> RetreivePhotosByGalleryId(int galleryId)
        {
            List<Photo> photos = photoDb.RetreivePhotosByGalleryId(galleryId, "Photo_RetrievePhotosByGalleryId");
            return photos;
        }

        /// <summary>
        /// Retrieve all photos by userId
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserID(int userID)
        {
            List<Photo> photos = photoDb.RetrievePhotosByUserID(userID, "Photo_RetrieveAllPhotosByUserId");
            return photos;
        }

        /// <summary>
        /// Retrieve all photos by uploaded by a user in a specific gallery
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryId)
        {
            List<Photo> photos = photoDb.RetrievePhotosByUserIDAndGalleryId(userID, galleryId, "Photo_RetrieveAllPhotosByUserIdAndGalleryId");
            return photos;
        }

        /// <summary>
        /// Retrieve the orginal photo uploaded by the PhotoId
        /// </summary>
        /// <param name="photoId">Id of the row in the photo database</param>
        /// <returns></returns>
        public byte[] RetrieveOriginalByPhotoId(int photoId)
        {
            byte[] image = photoDb.RetrieveOriginalByPhotoId(photoId, "Photo_RetrieveOriginalByPhotoID");
            return image;
        }

        /// <summary>
        /// Select a random photo from a spefic user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RetrieveRandomPhotoId(int userId)
        {
            int photoId = photoDb.RetrieveRandomPhotoId(userId, "Photo_SelectRandomImageByUserID");
            return photoId;
        }

        /// <summary>
        /// Retrieve the last photo added
        /// </summary>
        /// <returns></returns>
        public Photo RetrieveLatestPhoto()
        {
            Photo photo = photoDb.RetrieveLatestPhoto("Photo_RetrieveLatestPhoto");
            return photo;
        }
    }
}
