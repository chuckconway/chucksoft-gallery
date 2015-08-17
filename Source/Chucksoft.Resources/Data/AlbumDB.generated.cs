using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chucksoft.Entities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
	internal partial class AlbumDB
	{
	    /// <summary>
        /// Inserts Album into the Albums Table
        /// </summary>
        /// <param name="album">A new populated album.</param>
        /// <returns>Insert Count</returns>
        public int Insert(Album album, string commandText)
        {
			DbParameter[] parameters = 
			{
					DbClient.MakeParameter("@Name",DbType.String,50,album.Name),
					DbClient.MakeParameter("@Description",DbType.String,255,album.Description),
                    DbClient.MakeParameter("@UserId",DbType.Int32,4,album.UserId)

			};

            return DbClient.NonQuery(commandText, parameters);

        }

	
	    /// <summary>
        /// Updates the Album table by the primary key, if the Album is dirty then an update will occur
        /// </summary>
        /// <param name="album">a populated album</param>
        /// <returns>update count</returns>
        public int Update(Album album, string commandText)
        {
            int updateCount = 0;

            if(album.IsDirty())
            {
				DbParameter[] parameters = 
				{
					DbClient.MakeParameter("@AlbumID",DbType.Int32,4,album.AlbumID),
					DbClient.MakeParameter("@Name",DbType.String,50,album.Name),
					DbClient.MakeParameter("@Description",DbType.String,150,album.Description),
                    DbClient.MakeParameter("@UserId",DbType.Int32,4,album.UserId)

				};

                updateCount = DbClient.NonQuery(commandText, parameters);

            }

            return updateCount;
        }
        
        /// <summary>
        /// Delete a Album by the primary key
        /// </summary>
        /// <param name="album"></param>
        public int Delete(Album album, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@AlbumID",DbType.Int32,4,album.AlbumID),

			};

            return DbClient.NonQuery(commandText, parameters);

        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal Album Populate(SqlDataReader reader)
        {
            Album album = new Album();

            using (reader)
            {
                while (reader.Read())
                {
                    album = GetItem(reader);                    
                }
            }

            return album;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private Album GetItem(IDataRecord reader)
        {
            Album album = new Album
                              {
                                  AlbumID = Convert.ToInt32(reader["AlbumID"]),
                                  Name = Convert.ToString(reader["Name"]),
                                  Description = Convert.ToString(reader["Description"]),
                                  UserId = Convert.ToInt32(reader["UserId"])
                              };


            return album;
        }

        /// <summary>
        /// Populates the albums.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal List<Album> PopulateAlbums(DbDataReader reader)
        {
            List<Album> albums = new List<Album>();

            using (reader)
            {
                while (reader.Read())
                {
                    Album album = GetItem(reader);
                    albums.Add(album);
                }
            }

            return albums;
       }
    }
}
