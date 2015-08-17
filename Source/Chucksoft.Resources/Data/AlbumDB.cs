using System.Collections.Generic;
using System.Data.SqlClient;
using Chucksoft.Entities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
    internal partial class AlbumDB
    {
        /// <summary>
        /// Gets or sets the db client.
        /// </summary>
        /// <value>The db client.</value>
        public DbClient DbClient { get; set; }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
		public List<Album> SelectAll(string commandText)
		{
            DbDataReader reader = DbClient.Reader(commandText);
            List<Album> albums = PopulateAlbums(reader);

		    return albums;
		}

        /// <summary>
        /// Delete multipule albums
        /// </summary>
        /// <param name="albums">Collection of albums to be removed.</param>
        public void Delete(List<Album> albums, string commandText)
        {
            foreach (Album album in albums)
            {
                Delete(album, commandText);
            }
        }
    }
}