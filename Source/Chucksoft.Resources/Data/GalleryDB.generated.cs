using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chucksoft.Entities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
	internal partial class GalleryDB
	{
	    /// <summary>
        /// Inserts Gallery into the Gallerys Table
        /// </summary>
        /// <param name="gallery">A new populated gallery.</param>
        /// <returns>Insert Count</returns>
        public int Insert(Gallery gallery, string commandText)
        {
			DbParameter[] parameters = 
			{
					DbClient.MakeParameter("@Name",DbType.String,50,gallery.Name),
					DbClient.MakeParameter("@Description",DbType.String,1024,gallery.Description),
					DbClient.MakeParameter("@AlbumID",DbType.Int32,4,gallery.AlbumID),
                    DbClient.MakeParameter("@UserId",DbType.Int32,4,gallery.UserId)
			};

            return DbClient.NonQuery(commandText, parameters);
        }

	
	    /// <summary>
        /// Updates the Gallery table by the primary key, if the Gallery is dirty then an update will occur
        /// </summary>
        /// <param name="gallery">a populated gallery</param>
        /// <returns>update count</returns>
        public int Update(Gallery gallery, string commandText)
        {
            int updateCount = 0;

            if(gallery.IsDirty())
            {
				DbParameter[] parameters = 
				{
					DbClient.MakeParameter("@GalleryID",DbType.Int32,4,gallery.GalleryID),
					DbClient.MakeParameter("@Name",DbType.String,50,gallery.Name),
					DbClient.MakeParameter("@Description",DbType.String,150,gallery.Description),
					DbClient.MakeParameter("@AlbumID",DbType.Int32,4,gallery.AlbumID),
                    DbClient.MakeParameter("@UserId",DbType.Int32,4,gallery.UserId)

				};

                updateCount = DbClient.NonQuery(commandText, parameters);

            }

            return updateCount;
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal Gallery Populate(SqlDataReader reader)
        {
            Gallery gallery = new Gallery();

            using (reader)
            {
                while (reader.Read())
                {
                    gallery = GetItem(reader);                    
                }
            }

            return gallery;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private Gallery GetItem(IDataRecord reader)
        {
            Gallery gallery = new Gallery
            {
                GalleryID = Convert.ToInt32(reader["GalleryID"]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                AlbumID = Convert.ToInt32(reader["AlbumID"]),
                UserId = Convert.ToInt32(reader["UserId"])
            };

            return gallery;
        }

	    internal delegate Gallery GalleryItem(IDataRecord reader);
        /// <summary>
        /// Populates the gallerys.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="galleryItemMethod">The gallery item method.</param>
        /// <returns></returns>
        internal List<Gallery> PopulateGallerys(DbDataReader reader, GalleryItem galleryItemMethod)
        {
            List<Gallery> gallerys = new List<Gallery>();

            using (reader)
            {
                while (reader.Read())
                {
                    Gallery gallery = galleryItemMethod(reader);
                    gallerys.Add(gallery);
                }
            }

            return gallerys;
        }
    }
}
