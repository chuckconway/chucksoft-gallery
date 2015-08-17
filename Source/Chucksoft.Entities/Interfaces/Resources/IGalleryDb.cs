using System.Collections.Generic;


namespace Chucksoft.Entities.Interfaces.Resources
{
    public interface IGalleryDb
    {
        /// <summary>
        /// Get the gallery by it's Id
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        Gallery RetrieveGalleryByGalleryId(int galleryId);

        /// <summary>
        /// Gets all galleries without the photo count
        /// </summary>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleries();

        /// <summary>
        /// Get's all Galleries by with Photocount
        /// </summary>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesWithPhotoCount();

        /// <summary>
        /// Delete more than one item at a time
        /// </summary>
        /// <param name="keys">The keys.</param>
        void DeleteItems(List<int> keys);

        void Delete(int galleryId);

        /// <summary>
        /// Gets all galleires by UserID with there photo coutn
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId);

        /// <summary>
        /// Inserts Gallery into the Gallerys Table
        /// </summary>
        /// <param name="gallery">A new populated gallery.</param>
        /// <returns>Insert Count</returns>
        int Insert(Gallery gallery);

        /// <summary>
        /// Updates the Gallery table by the primary key, if the Gallery is dirty then an update will occur
        /// </summary>
        /// <param name="gallery">a populated gallery</param>
        /// <returns>update count</returns>
        int Update(Gallery gallery);

        /// <summary>
        /// Retrieves all galleries by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        List<Gallery> RetrieveAllGalleriesByUserId(int userId);
    }
}