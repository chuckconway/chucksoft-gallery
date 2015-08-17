using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlCe
{
    public class AlbumSqlCe : IAlbum
    {
        AlbumDB albumDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumSqlCe"/> class.
        /// </summary>
        public AlbumSqlCe()
        {
            albumDb = new AlbumDB();
            albumDb.DbClient = new SqlCeClient();
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<Album> SelectAll()
        {
            const string sql = "Select AlbumID, Name, Description, UserId From Album  Order by Name asc";

            List<Album> albums = albumDb.SelectAll(sql);
            return albums;
        }


        private string deleteMethod = "Delete From Album Where AlbumID = @AlbumID ";

        /// <summary>
        /// Delete a Album by the primary key
        /// </summary>
        /// <param name="album"></param>
        public int Delete(Album album)
        {
            return albumDb.Delete(album, deleteMethod);
        }

        /// <summary>
        /// Delete multipule albums
        /// </summary>
        /// <param name="albums">Collection of albums to be removed.</param>
        public void Delete(List<Album> albums)
        {
            albumDb.Delete(albums, deleteMethod);
        }

        /// <summary>
        /// Inserts Album into the Albums Table
        /// </summary>
        /// <param name="album">A new populated album.</param>
        /// <returns>Insert Count</returns>
        public int Insert(Album album)
        {
            const string sql = "INSERT INTO Album (Name, Description, UserId) VALUES (@Name, @Description, @UserId)";
            return albumDb.Insert(album, sql);
        }


        /// <summary>
        /// Updates the Album table by the primary key, if the Album is dirty then an update will occur
        /// </summary>
        /// <param name="album">a populated album</param>
        /// <returns>update count</returns>
        public int Update(Album album)
        {
            const string sql = "Update Album SET Name = @Name, 	Description = @Description, UserId = @UserId Where AlbumID = @AlbumID";
            return albumDb.Update(album, sql);
        }
    }
}
