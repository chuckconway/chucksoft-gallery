using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Chucksoft.Resources.Data.SqlServer
{
    public class GallerySql : IGallery 
    {
        GalleryDB galleryDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="GallerySql"/> class.
        /// </summary>
        public GallerySql()
        {
            galleryDb = new GalleryDB();
            galleryDb.DbClient = new SqlServerClient();
        }

        /// <summary>
        /// Gallery Retrieve Gallery By gallery ID
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public Gallery RetrieveGalleryByGalleryId(int galleryId)
        {
            Gallery gallery = galleryDb.RetrieveGalleryByGalleryId(galleryId, "Gallery_SelectByPrimaryKey");
            return gallery;
        }

        /// <summary>
        /// Retreives all galleries, without photocount
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleries()
        {
            List<Gallery> gallery = galleryDb.RetrieveAllGalleries("Gallery_SelectAll");
            return gallery;
        }

        /// <summary>
        /// Get all Galleries with Photo count
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesWithPhotoCount()
        {
            List<Gallery> gallery = galleryDb.RetrieveAllGalleriesWithPhotoCount("Gallery_SelectAllGalleriesWithPhotoCount");
            return gallery;
        }

        private string deleteGalleryMethod = "Gallery_DeleteGalleryAndMovePhotos";

        /// <summary>
        /// Delete a Gallery by the primary key
        /// </summary>
        /// <param name="galleryId"></param>
        public int Delete(int galleryId)
        {
            return galleryDb.Delete(galleryId, deleteGalleryMethod);
        }

        /// <summary>
        /// Delete more than one item at a time
        /// </summary>
        /// <param name="keys"></param>
        public void DeleteItems(List<int> keys)
        {
            foreach (int key in keys)
            {
                galleryDb.Delete(key, deleteGalleryMethod);
            }
        }

        /// <summary>
        /// Retrieves all the GAlleries by a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserId(int userId)
        {
            List<Gallery> galleries = galleryDb.RetrieveAllGalleriesByUserId(userId, "Gallery_SelectAllGalleriesByUserId");
            return galleries;
        }

        /// <summary>
        /// Retrieves galleries by user with how many photos are in the gallery
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId)
        {
            List<Gallery> galleries = galleryDb.RetrieveAllGalleriesByUserIdWithPhotoCount(userId, "Gallery_SelectAllGalleriesByUserIdWithPhotoCount");
            return galleries;
        }

        /// <summary>
        /// Inserts Gallery into the Gallerys Table
        /// </summary>
        /// <param name="gallery">A new populated gallery.</param>
        /// <returns>Insert Count</returns>
        public int Insert(Gallery gallery)
        {
            return galleryDb.Insert(gallery, "Gallery_Insert");
        }


        /// <summary>
        /// Updates the Gallery table by the primary key, if the Gallery is dirty then an update will occur
        /// </summary>
        /// <param name="gallery">a populated gallery</param>
        /// <returns>update count</returns>
        public int Update(Gallery gallery)
        {
            int updateCount = galleryDb.Update(gallery, "Gallery_Update");
            return updateCount;
        }
    }
}
