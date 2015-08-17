using System;
using System.Collections.Generic;
using Chucksoft.Entities;

namespace Chucksoft.Resources
{
    public interface IGallery
    {
        /// <summary>
        /// Deletes the specified gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        int Delete(int galleryId);

        /// <summary>
        /// Deletes the items.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void DeleteItems(List<int> keys);

        /// <summary>
        /// Inserts the specified gallery.
        /// </summary>
        /// <param name="gallery">The gallery.</param>
        /// <returns></returns>
        int Insert(Gallery gallery);

        /// <summary>
        /// Retrieves all galleries.
        /// </summary>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleries();

        /// <summary>
        /// Retrieves all galleries by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesByUserId(int userId);

        /// <summary>
        /// Retrieves all galleries by user id with photo count.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId);

        /// <summary>
        /// Retrieves all galleries with photo count.
        /// </summary>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesWithPhotoCount();

        /// <summary>
        /// Retrieves the gallery by gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        Gallery RetrieveGalleryByGalleryId(int galleryId);

        /// <summary>
        /// Updates the specified gallery.
        /// </summary>
        /// <param name="gallery">The gallery.</param>
        /// <returns></returns>
        int Update(Gallery gallery);
    }
}
