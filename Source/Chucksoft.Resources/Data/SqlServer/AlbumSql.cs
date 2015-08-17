using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlServer
{
    public class AlbumSql : IAlbum
    {
        AlbumDB albumDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumSql"/> class.
        /// </summary>
        public AlbumSql()
        {
            albumDb = new AlbumDB();
            albumDb.DbClient = new SqlServerClient();
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<Album> SelectAll()
        {
            List<Album> albums = albumDb.SelectAll("Album_SelectAll");
            return albums;
        }


        private string deleteMethod = "Album_Delete";

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
            return albumDb.Insert(album, "Album_Insert");
        }


        /// <summary>
        /// Updates the Album table by the primary key, if the Album is dirty then an update will occur
        /// </summary>
        /// <param name="album">a populated album</param>
        /// <returns>update count</returns>
        public int Update(Album album)
        {
            return albumDb.Update(album, "Album_Update");
        }


    }
}
