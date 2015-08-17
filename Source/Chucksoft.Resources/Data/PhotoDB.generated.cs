using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chucksoft.Entities;
using Conway.Utilities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
	internal partial class PhotoDB
	{
	    /// <summary>
        /// Inserts Photo into the Photos Table
        /// </summary>
        /// <param name="photo">A new populated photo.</param>
        /// <param name="galleryPhoto"></param>
        /// <param name="adminPhotos">Photos to be used in admin section</param>
        /// <returns>Insert Count</returns>
        public int Insert(Photo photo, GalleryPhoto galleryPhoto, GalleryPhoto adminPhotos, string commandText)
        {
            DbParameter[] parameters = 
			{
					DbClient.MakeParameter("@Title",DbType.String,100,photo.Title),
					DbClient.MakeParameter("@Description",DbType.String,255,photo.Description),
					DbClient.MakeParameter("@DateTaken",DbType.DateTime,8,photo.DateTaken),
					DbClient.MakeParameter("@GalleryID",DbType.Int32,4,photo.GalleryID),
					DbClient.MakeParameter("@OriginalImage",DbType.Binary, galleryPhoto.OriginalImage.Length, galleryPhoto.OriginalImage),
					DbClient.MakeParameter("@DisplayImage",DbType.Binary, galleryPhoto.DisplayImage.Length, galleryPhoto.DisplayImage),
					DbClient.MakeParameter("@ThumbnailImage",DbType.Binary, galleryPhoto.ThumbnailImage.Length, galleryPhoto.ThumbnailImage),
                    DbClient.MakeParameter("@AdminThumbnail",DbType.Binary, adminPhotos.ThumbnailImage.Length, adminPhotos.ThumbnailImage),
                    DbClient.MakeParameter("@AdminFullsizeImage",DbType.Binary, adminPhotos.DisplayImage.Length, adminPhotos.DisplayImage),
                    DbClient.MakeParameter("@Profile",DbType.String, 50, photo.Profile)

			};

            return DbClient.NonQuery(commandText, parameters);

        }

	
	    /// <summary>
        /// Updates the Photo table by the primary key, if the Photo is dirty then an update will occur
        /// </summary>
        /// <param name="photo">a populated photo</param>
        /// <returns>update count</returns>
        public int UpdateMetaData(Photo photo, string commandText)
        {
            int updateCount = 0;

            if(photo.IsDirty())
            {
                DbParameter[] parameters = 
				{
					DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photo.PhotoID),
					DbClient.MakeParameter("@Title",DbType.String,100,photo.Title),
					DbClient.MakeParameter("@Description",DbType.String,255,photo.Description),
					DbClient.MakeParameter("@DateTaken",DbType.DateTime,8,photo.DateTaken)
				};

                updateCount = DbClient.NonQuery(commandText, parameters);

            }

            return updateCount;
        }

        /// <summary>
        /// Updates the photos in the database
        /// </summary>
        /// <param name="photoID">Id of photo row</param>
        /// <param name="photos">the class containing the resized/new images</param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int UpdateImages(int photoID, GalleryPhoto photos, string commandText)
        {
            DbParameter[] parameters = 
				{
					DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photoID),
                    DbClient.MakeParameter("@OriginalImage",DbType.Binary, photos.OriginalImage.Length, photos.OriginalImage),
                    DbClient.MakeParameter("@DisplayImage",DbType.Binary, photos.DisplayImage.Length, photos.DisplayImage),
                    DbClient.MakeParameter("@ThumbnailImage",DbType.Binary, photos.ThumbnailImage.Length, photos.ThumbnailImage)

				};

            int updateCount = DbClient.NonQuery(commandText, parameters);

            return updateCount;

        }

       
        
        /// <summary>
        /// Delete a Photo by the primary key
        /// </summary>
        /// <param name="photo"></param>
        public int Delete(Photo photo, string commandText)
        {
			DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photo.PhotoID),

			};

            return DbClient.NonQuery(commandText, parameters);
        }
        
        /// <summary>
        /// Retrieves the first photo from the reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal Photo Populate(DbDataReader reader)
        {
            Photo photo = new Photo();

            using (reader)
            {
                while (reader.Read())
                {
                    photo = GetItem(reader);                    
                }
            }

            return photo;
        }

        /// <summary>
        /// populates the metadata of a photo
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Photo GetItem(IDataRecord reader)
        {
            Photo photo = new Photo
                              {
                                  PhotoID = Convert.ToInt32(reader["PhotoID"]),
                                  Title = Convert.ToString(reader["Title"]),
                                  Description = Convert.ToString(reader["Description"]),
                                  DateTaken = SetDateTaken(reader),
                                  GalleryID = Convert.ToInt32(reader["GalleryID"]),
                                  Profile = EnumParse<PhotoProfile>.Parse(reader["Profile"].ToString())
                              };

            return photo;
        }


        /// <summary>
        /// Retrieves a collection of photos from a reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal List<Photo> PopulatePhotos(DbDataReader reader)
        {
            List<Photo> photos = new List<Photo>();

            using (reader)
            {
                while (reader.Read())
                {
                    Photo photo = GetItem(reader);
                    photos.Add(photo);
                }
            }

            return photos;
       }
    }
}
