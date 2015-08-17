using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chucksoft.Entities;
using Chucksoft.Entities.Interfaces.Resources;

namespace Chucksoft.Resources.Database
{
    public class GalleryDb : IGalleryDb
    {
        private readonly ISqlServer _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryDb"/> class.
        /// </summary>
        public GalleryDb(): this(new SqlServer()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryDb"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public GalleryDb(ISqlServer database)
        {
            _database = database;
        }

        /// <summary>
        /// Get the gallery by it's Id
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public Gallery RetrieveGalleryByGalleryId(int galleryId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@GalleryId",SqlDbType.Int,4, galleryId)
			};

            GalleryItem items = GetItemUserId;
            SqlDataReader reader = _database.Reader("Gallery_SelectByPrimaryKey", parameters);
            Gallery gallery = Populate(reader, items);

            return gallery;
        }

        ///// <summary>
        ///// Gets all the galleries a user has
        ///// </summary>
        ///// <param name="userId">The user id.</param>
        ///// <returns></returns>
        //private List<Gallery> RetrieveAllGalleriesByUserId(int userId)
        //{
        //    SqlParameter[] parameters = 
        //    {
        //        _database.MakeParameter("@UserId",SqlDbType.Int, 4, userId),

        //    };

        //    GalleryItem items = GetItemUserId;
        //    SqlDataReader reader = _database.Reader("Gallery_SelectAllGalleriesByUserId", parameters);
        //    List<Gallery> gallery = PopulateGallerys(reader, items);

        //    return gallery;
        //}

        /// <summary>
        /// Gets all galleries without the photo count
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleries()
        {
            GalleryItem items = GetItemUserId;
            SqlDataReader reader = _database.Reader("Gallery_SelectAll");
            List<Gallery> gallery = PopulateGallerys(reader, items);

            return gallery;
        }

        /// <summary>
        /// Get's all Galleries by with Photocount
        /// </summary>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesWithPhotoCount()
        {
            //"Gallery_SelectAllGalleriesWithPhotoCount"
            GalleryItem items = GetItemWithPhotoCount;
            SqlDataReader reader = _database.Reader("Gallery_SelectAllGalleriesWithPhotoCount");
            List<Gallery> gallery = PopulateGallerys(reader, items);

            return gallery;
        }

        /// <summary>
        /// Delete more than one item at a time
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void DeleteItems(List<int> keys)
        {
            foreach (int key in keys)
            {
                Delete(key, "Gallery_Delete");
            }
        }

        public void Delete(int galleryId)
        {
            Delete(galleryId, "Gallery_Delete");
        }

        /// <summary>
        /// Delete a Gallery by the primary key
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        private void Delete(int galleryId, string commandText)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@GalleryId",SqlDbType.Int,4, galleryId)
			};

             _database.NonQuery(commandText, parameters);
        }

        /// <summary>
        /// Gets all galleires by UserID with there photo coutn
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserIdWithPhotoCount(int userId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@UserId",SqlDbType.Int, 4, userId),

			};

            GalleryItem items = GetItemWithPhotoCountAndUserId;
            SqlDataReader reader = _database.Reader("Gallery_SelectAllGalleriesByUserIdWithPhotoCount", parameters);
            List<Gallery> galleries = PopulateGallerys(reader, items);

            return galleries;
        }

        /// <summary>
        /// Get photo count by userID
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static Gallery GetItemWithPhotoCountAndUserId(SqlDataReader reader)
        {
            Gallery gallery = new Gallery
                                  {
                                      GalleryId = Convert.ToInt32(reader["GalleryID"]),
                                      Name = Convert.ToString(reader["Name"]),
                                      Description = Convert.ToString(reader["Description"]),
                                      UserId = Convert.ToInt32(reader["UserId"]),
                                      PhotoCount = (reader["PhotoCount"] != DBNull.Value ? Convert.ToInt32(reader["PhotoCount"]) : 0)
                                  };

            return gallery;
        }

        /// <summary>
        /// No photo count. Need to add some extra logic to deal with the db.Null
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static Gallery GetItemWithPhotoCount(SqlDataReader reader)
        {
            Gallery gallery = new Gallery
                                  {
                                      GalleryId = Convert.ToInt32(reader["GalleryID"]),
                                      Name = Convert.ToString(reader["Name"]),
                                      Description = Convert.ToString(reader["Description"]),
                                      PhotoCount = (reader["PhotoCount"] != DBNull.Value ? Convert.ToInt32(reader["PhotoCount"]) : 0)
                                  };

            return gallery;
        }

        /// <summary>
        /// Inserts Gallery into the Gallerys Table
        /// </summary>
        /// <param name="gallery">A new populated gallery.</param>
        /// <returns>Insert Count</returns>
        public int Insert(Gallery gallery)
        {
            SqlParameter[] parameters = 
			{
					_database.MakeParameter("@Name",SqlDbType.NVarChar,50,gallery.Name),
					_database.MakeParameter("@Description",SqlDbType.NVarChar,1024,gallery.Description),
                    _database.MakeParameter("@UserId",SqlDbType.Int,4,gallery.UserId)
			};

            return _database.NonQuery("Gallery_Insert", parameters);
        }


        /// <summary>
        /// Updates the Gallery table by the primary key, if the Gallery is dirty then an update will occur
        /// </summary>
        /// <param name="gallery">a populated gallery</param>
        /// <returns>update count</returns>
        public int Update(Gallery gallery)
        {
            SqlParameter[] parameters = 
				{
					_database.MakeParameter("@GalleryID",SqlDbType.Int,4,gallery.GalleryId),
					_database.MakeParameter("@Name",SqlDbType.NVarChar,50,gallery.Name),
					_database.MakeParameter("@Description",SqlDbType.NVarChar,150,gallery.Description),
                    _database.MakeParameter("@UserId",SqlDbType.Int,4,gallery.UserId)
				};

            int updateCount = _database.NonQuery("Gallery_Update", parameters);

            return updateCount;
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        internal static Gallery Populate(SqlDataReader reader, GalleryItem item)
        {
            Gallery gallery = new Gallery();

            using (reader)
            {
                while (reader.Read())
                {
                    gallery = item(reader);
                }
            }

            return gallery;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static Gallery GetItemUserId(SqlDataReader reader)
        {
            Gallery gallery = new Gallery
            {
                GalleryId = Convert.ToInt32(reader["GalleryID"]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                UserId = Convert.ToInt32(reader["UserId"])
            };

            return gallery;
        }

        internal delegate Gallery GalleryItem(SqlDataReader reader);
        /// <summary>
        /// Populates the gallerys.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        internal static List<Gallery> PopulateGallerys(SqlDataReader reader, GalleryItem item)
        {
            List<Gallery> gallerys = new List<Gallery>();

            using (reader)
            {
                while (reader.Read())
                {
                    Gallery gallery = item(reader);
                    gallerys.Add(gallery);
                }
            }

            return gallerys;
        }


        #region IGalleryDb Members


        /// <summary>
        /// Retrieves all galleries by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public List<Gallery> RetrieveAllGalleriesByUserId(int userId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@UserId",SqlDbType.Int, 4, userId),

			};

            GalleryItem galleryItemMethod = GetItemUserId;

            SqlDataReader reader = _database.Reader("Gallery_SelectAllGalleriesByUserId", parameters);
            List<Gallery> gallery = PopulateGallerys(reader, galleryItemMethod);

            return gallery;
        }

        #endregion
    }
}
