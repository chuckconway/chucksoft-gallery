using System;
using Chucksoft.Entities;
using System.Collections.Generic;

namespace Chucksoft.Resources
{
    public interface IAlbum
    {
        /// <summary>
        /// Deletes the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        int Delete(Album album);

        /// <summary>
        /// Deletes the specified albums.
        /// </summary>
        /// <param name="albums">The albums.</param>
        void Delete(List<Album> albums);

        /// <summary>
        /// Inserts the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        int Insert(Album album);

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        List<Album> SelectAll();

        /// <summary>
        /// Updates the specified album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns></returns>
        int Update(Album album);
    }
}
