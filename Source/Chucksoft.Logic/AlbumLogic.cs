using System.Collections.Generic;
using Chucksoft.Entities;
using Chucksoft.Resources.Data;
using Chucksoft.Resources;

namespace Chucksoft.Logic
{
    public class AlbumLogic
    {
        /// <summary>
        /// Add new Album
        /// </summary>
        /// <param name="album"></param>
		public static void Add(Album album)
		{
            IAlbum userResource = Resource.GetResource<IAlbum>();
            userResource.Insert(album);
		}

        /// <summary>
        /// Delete a single Album
        /// </summary>
        /// <param name="album"></param>
        public static void Delete(Album album)
        {
            IAlbum userResource = Resource.GetResource<IAlbum>();
            userResource.Delete(album);
        }

        /// <summary>
        /// Delete multipule Albums
        /// </summary>
        /// <param name="albums"></param>
        public static void Delete(List<Album> albums)
        {
            IAlbum userResource = Resource.GetResource<IAlbum>();
            userResource.Delete(albums);
        }

        /// <summary>
        /// Updates the Album by the ablum ID
        /// </summary>
        /// <param name="album">Desired changes</param>
        public static void Update(Album album)
        {
            IAlbum userResource = Resource.GetResource<IAlbum>();
            userResource.Update(album);
        }

        /// <summary>
        /// Retrieve all albums
        /// </summary>
        /// <returns></returns>
        public static List<Album> GetAllAlbums()
        {
            IAlbum userResource = Resource.GetResource<IAlbum>();
            List<Album> ablums = userResource.SelectAll();

            return ablums;
        }
    }
}