using System;
using System.Collections.Generic;
using Chucksoft.Entities;

namespace Chucksoft.Resources
{
    public interface IPhoto
    {
        /// <summary>
        /// Deletes the specified photos.
        /// </summary>
        /// <param name="photos">The photos.</param>
        void Delete(List<Photo> photos);

        /// <summary>
        /// Deletes the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        void Delete(Photo photo);

        /// <summary>
        /// Inserts the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="galleryPhoto">The gallery photo.</param>
        /// <param name="adminPhotos">The admin photos.</param>
        /// <returns></returns>
        int Insert(Photo photo, GalleryPhoto galleryPhoto, GalleryPhoto adminPhotos);

        /// <summary>
        /// Moves the photos to new gallery.
        /// </summary>
        /// <param name="photos">The photos.</param>
        void MovePhotosToNewGallery(List<Photo> photos);

        /// <summary>
        /// Moves the photo to new gallery.
        /// </summary>
        /// <param name="photo">The photo.</param>
        void MovePhotoToNewGallery(Photo photo);

        /// <summary>
        /// Retreives the photos by gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        List<Photo> RetreivePhotosByGalleryId(int galleryId);

        /// <summary>
        /// Retrieves the admin fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveAdminFullsizeByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the admin thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveAdminThumbnailByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveFullsizeByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the latest photo.
        /// </summary>
        /// <returns></returns>
        Photo RetrieveLatestPhoto();

        /// <summary>
        /// Retrieves the original by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveOriginalByPhotoId(int photoId);

        /// <summary>
        /// Retrieves the photos by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        List<Photo> RetrievePhotosByUserID(int userID);

        /// <summary>
        /// Retrieves the photos by user ID and gallery id.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryId);

        /// <summary>
        /// Retrieves the random photo.
        /// </summary>
        /// <returns></returns>
        Photo RetrieveRandomPhoto();

        /// <summary>
        /// Retrieves the random photo by gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        Photo RetrieveRandomPhotoByGalleryId(int galleryId);

        /// <summary>
        /// Retrieves the random photo id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        int RetrieveRandomPhotoId(int userId);

        /// <summary>
        /// Retrieves the thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        byte[] RetrieveThumbnailByPhotoId(int photoId);

        /// <summary>
        /// Updates the images.
        /// </summary>
        /// <param name="photoID">The photo ID.</param>
        /// <param name="photos">The photos.</param>
        /// <returns></returns>
        int UpdateImages(int photoID, GalleryPhoto photos);

        /// <summary>
        /// Updates the meta data.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        int UpdateMetaData(Photo photo);
    }
}
