using System.Collections.Generic;
using Chucksoft.Entities;

using Chucksoft.Entities.Interfaces.Resources;
using Chucksoft.Resources.Database;

namespace Chucksoft.Logic
{
    public class GalleryLogic
    {
        private readonly IGalleryDb _resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryLogic"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public GalleryLogic(IGalleryDb resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryLogic"/> class.
        /// </summary>
        public GalleryLogic() : this(new GalleryDb()) {}

        /// <summary>
        /// Inserts the specified gallery.
        /// </summary>
        /// <param name="gallery">The gallery.</param>
		public void Insert(Gallery gallery)
		{
            _resource.Insert(gallery);
		}

        /// <summary>
        /// Retrieves the gallery by gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public Gallery RetrieveGalleryByGalleryId(int galleryId)
        {
            Gallery gallery = _resource.RetrieveGalleryByGalleryId(galleryId);
            return gallery;
        }

        /// <summary>
        /// Retrieves Galleries with photos only.
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveGalleriesWithPhotos()
        {
            List<Gallery> galleriesWithPhotos = new List<Gallery>();
            List<Gallery> allGalleries = _resource.RetrieveAllGalleriesWithPhotoCount();

            //filter out the galleries with out images
            foreach (Gallery gallery in allGalleries)
            {
                if (gallery.PhotoCount > 0)
                {
                    galleriesWithPhotos.Add(gallery);
                }
            }

            return galleriesWithPhotos;
        }

        /// <summary>
        /// Retrieves the galleries by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Gallery> RetrieveGalleriesByUserId(int userId)
        {
           List<Gallery> gallery = _resource.RetrieveAllGalleriesByUserId(userId);
           return gallery;
        }

        /// <summary>
        /// Retrieves all galleries by user id with photo count.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId)
        {
            List<Gallery> gallery = _resource.RetrieveAllGalleriesByUserIdWithPhotoCount(userId);
            return gallery; 
        }

        /// <summary>
        /// Retrieves all galleries.
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleries()
        {
            List<Gallery> galleries = _resource.RetrieveAllGalleries();
            return galleries;
        }

        /// <summary>
        /// Deletes the specified gallery id.
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        public void Delete(int galleryId)
        {
            _resource.Delete(galleryId);
        }

        /// <summary>
        /// Deletes the specified gallery ids.
        /// </summary>
        /// <param name="galleryIds">The gallery ids.</param>
        public  void Delete(List<int> galleryIds)
        {
            _resource.DeleteItems(galleryIds);
        }
    }
}