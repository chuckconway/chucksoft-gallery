using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlCe
{
    public class GallerySqlCe
    {
        GalleryDB galleryDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="GallerySqlCe"/> class.
        /// </summary>
        public GallerySqlCe()
        {
            galleryDb = new GalleryDB();
            galleryDb.DbClient = new SqlCeClient();
        }

        /// <summary>
        /// Gallery Retrieve Gallery By gallery ID
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public Gallery RetrieveGalleryByGalleryId(int galleryId)
        {
            const string sql = "Select GalleryID, Name, Description, AlbumID, UserId From Gallery Where GalleryID = @GalleryID ";

            Gallery gallery = galleryDb.RetrieveGalleryByGalleryId(galleryId, sql);
            return gallery;
        }

        /// <summary>
        /// Retreives all galleries, without photocount
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleries()
        {
            const string sql = "Select GalleryID, Name, Description, AlbumID, UserId From Gallery";

            List<Gallery> gallery = galleryDb.RetrieveAllGalleries(sql);
            return gallery;
        }

        /// <summary>
        /// Get all Galleries with Photo count
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesWithPhotoCount()
        {
           // const string sql = "";

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
            const string sql = @"Select GalleryID, [Name], [Description], AlbumId, UserId 
	        FROM Gallery
	        Where UserId = @UserId
	        Order BY [Name] asc";

            List<Gallery> galleries = galleryDb.RetrieveAllGalleriesByUserId(userId, sql);
            return galleries;
        }

        /// <summary>
        /// Retrieves galleries by user with how many photos are in the gallery
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId)
        {
           const string sql = @"Select G.GalleryID, [Name], [Description], G.AlbumId, G.UserId, PhotoCount
	        From Gallery G Inner Join
	        (
		        Select IG.GalleryID, IG.UserID, Count(PhotoID) as PhotoCount
		        From Photo Inner Join Gallery IG On Photo.GalleryID = IG.GalleryID
		        Where UserID = @UserID
		        Group BY IG.GalleryID, IG.UserID

	        ) as T1 ON G.GalleryID = T1.GalleryID
	        Where G.UserId = @UserID And PhotoCount > 0";

            List<Gallery> galleries = galleryDb.RetrieveAllGalleriesByUserIdWithPhotoCount(userId, sql);
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
