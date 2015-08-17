using System;
using System.Collections.Generic;
using System.Data;
using Chucksoft.Entities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
    internal partial class GalleryDB 
    {

        /// <summary>
        /// Gets or sets the db client.
        /// </summary>
        /// <value>The db client.</value>
        public DbClient DbClient { get; set; }

        /// <summary>
        /// Get the gallery by it's Id
        /// </summary>
        /// <param name="galleryId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public Gallery RetrieveGalleryByGalleryId(int galleryId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@GalleryId",DbType.Int32,4, galleryId)
			};

            GalleryItem items = GetItem;
            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<Gallery> galleries = PopulateGallerys(reader, items);

            Gallery gallery = new Gallery();
       
            if(galleries.Count > 0)
            {
                //only one should ever be returned.
               gallery = galleries[0];
            }

            return gallery;
        }

        /// <summary>
        /// Gets all the galleries a user has
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="procedure"></param>
        /// <param name="galleryItemMethod"></param>
        /// <returns></returns>
        private List<Gallery> RetrieveAllGalleriesByUserId(int userId, string procedure, GalleryItem galleryItemMethod)
		{
		    DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserId",DbType.Int32, 4, userId),

			};

            DbDataReader reader = DbClient.Reader(procedure, parameters);
            List<Gallery> gallery = PopulateGallerys(reader, galleryItemMethod);

		    return gallery;
		}
        
        /// <summary>
        /// Gets all galleries without the photo count
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleries(string commandText)
        {
            //Gallery_SelectAll
            GalleryItem items = GetItem;
            DbDataReader reader = DbClient.Reader(commandText);
            List<Gallery> gallery = PopulateGallerys(reader, items);

            return gallery;
        }

        /// <summary>
        /// Get's all Galleries by with Photocount
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesWithPhotoCount(string commandText)
        {
            GalleryItem items = GetItemWithPhotoCount;
            DbDataReader reader = DbClient.Reader(commandText);
            List<Gallery> gallery = PopulateGallerys(reader, items);

            return gallery;
        }

        /// <summary>
        /// Delete more than one item at a time
        /// </summary>
        /// <param name="keys"></param>
        public void DeleteItems(List<int> keys, string commandText )
        {
            foreach (int key in keys)
            {
                Delete(key, commandText);
            }
        }

        /// <summary>
        /// Delete a Gallery by the primary key
        /// </summary>
        /// <param name="galleryId"></param>
        public int Delete(int galleryId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@GalleryId",DbType.Int32,4, galleryId)
			};

            return DbClient.NonQuery(commandText, parameters);
        }

        /// <summary>
        /// Gets all galleries by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserId(int userId, string commandText)
        {
            GalleryItem items = GetItem;

            return RetrieveAllGalleriesByUserId(userId, commandText, items);
        }

        /// <summary>
        /// Gets all galleires by UserID with there photo coutn
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId, string commandText)
        {
            GalleryItem getPhotoCountItems = GetItemWithPhotoCountAndUserId;
            return RetrieveAllGalleriesByUserId(userId, commandText, getPhotoCountItems); 
        }

        /// <summary>
        /// Get photo count by userID
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Gallery GetItemWithPhotoCountAndUserId(IDataRecord reader)
        {
            Gallery gallery = new Gallery
            {
                GalleryID = Convert.ToInt32(reader["GalleryID"]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                AlbumID = Convert.ToInt32(reader["AlbumID"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                PhotoCount = (reader["PhotoCount"] != DBNull.Value ? Convert.ToInt32(reader["PhotoCount"]) : 0)
            };

            return gallery;
        }

        /// <summary>
        /// No photo count. Need to add some extra logic to deal with the db.Null
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Gallery GetItemWithPhotoCount(IDataRecord reader)
        {
            Gallery gallery = new Gallery
            {
                GalleryID = Convert.ToInt32(reader["GalleryID"]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                AlbumID = Convert.ToInt32(reader["AlbumID"]),
                PhotoCount = (reader["PhotoCount"] != DBNull.Value ? Convert.ToInt32(reader["PhotoCount"]) : 0)
            };

            return gallery;
        }

        //public abstract int Delete(int galleryId);
    }
}