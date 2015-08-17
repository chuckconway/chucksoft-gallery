using System.Collections.Generic;

namespace Chucksoft.Entities.Interfaces.Resources
{
    public interface IPhotoDb
    {
        /// <summary>
        /// Retrieves the fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveFullsizeByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the admin thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveAdminThumbnailByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the admin fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveAdminFullsizeByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveThumbnailByPhotoId(int photoId);

        /// <summary>
        /// Retrieves a random photo from a specified gallery
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        Photo RetrieveRandomPhotoByGalleryId(int galleryId);

        /// <summary>
        /// Gets a random photo from the photo table
        /// </summary>
        /// <returns></returns>
        Photo RetrieveRandomPhoto();

        /// <summary>
        /// Gets one image by photo Id
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="procedure"></param>
        /// <returns></returns>
        byte[] GetImage(int photoId, string procedure);

        /// <summary>
        /// Moves a collection of photos to a new gallery
        /// </summary>
        /// <param name="photos">The photos.</param>
        void MovePhotosToNewGallery(List<Photo> photos);

        /// <summary>
        /// Moves a photo to a new gallery
        /// </summary>
        /// <param name="photo">The photo.</param>
        void MovePhotoToNewGallery(Photo photo);

        /// <summary>
        /// Retrieves all photos in a gallery
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        List<Photo> RetreivePhotosByGalleryId(int galleryId);

        /// <summary>
        /// Gets all photos a user has uploaded
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        List<Photo> RetrievePhotosByUserID(int userID);

        /// <summary>
        /// Get's all photos by userId and Galleryid
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryId);

        /// <summary>
        /// Get's the orginal photo uploaded by photo id
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveOriginalByPhotoId(int photoId);

        /// <summary>
        /// Gets a random photo by user id
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        int RetrieveRandomPhotoId(int userId);

        /// <summary>
        /// Gets the last photo added
        /// </summary>
        /// <returns></returns>
        Photo RetrieveLatestPhoto();

        /// <summary>
        /// Deletes a collection of Photos
        /// </summary>
        /// <param name="photos">Collection photos to be deleted</param>
        void Delete(List<Photo> photos);

        /// <summary>
        /// Inserts Photo into the Photos Table
        /// </summary>
        /// <param name="photo">A new populated photo.</param>
        /// <param name="galleryPhoto">The gallery photo.</param>
        /// <param name="adminPhotos">Photos to be used in admin section</param>
        /// <returns>Insert Count</returns>
        int Insert(Photo photo, GalleryPhoto galleryPhoto, GalleryPhoto adminPhotos);

        /// <summary>
        /// Updates the Photo table by the primary key, if the Photo is dirty then an update will occur
        /// </summary>
        /// <param name="photo">a populated photo</param>
        /// <returns>update count</returns>
        int UpdateMetaData(Photo photo);

        /// <summary>
        /// Updates the photos in the database
        /// </summary>
        /// <param name="photoID">Id of photo row</param>
        /// <param name="photos">the class containing the resized/new images</param>
        /// <returns></returns>
        int UpdateImages(int photoID, GalleryPhoto photos);

        /// <summary>
        /// Delete a Photo by the primary key
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        int Delete(Photo photo);
    }
}