using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Chucksoft.Entities;
using System.Data;
using Chucksoft.Entities.Interfaces.Resources;
using Conway.Utilities;

namespace Chucksoft.Resources.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class PhotoDb : IPhotoDb
    {
        private readonly ISqlServer _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoDb"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public PhotoDb(ISqlServer database)
        {
            _database = database;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoDb"/> class.
        /// </summary>
        public PhotoDb() : this(new SqlServer()) {}

        /// <summary>
        /// Retrieves a random photo from a specified gallery
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public Photo RetrieveRandomPhotoByGalleryId(int galleryId)
        {
            Photo photo = new Photo();

            SqlParameter[] parameters = 
			{
                _database.MakeParameter("@GalleryId",SqlDbType.Int,4,galleryId)
			};

            SqlDataReader reader = _database.Reader("Photo_RetreiveRandomPhotoByGalleryId", parameters);
            List<Photo> photos = PopulatePhotos(reader);

            if (photos.Count > 0)
            {
                photo = photos[0];
            }

            return photo;
        }

        /// <summary>
        /// Retrieves the admin thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public byte[] RetrieveAdminThumbnailByPhotoId(int photoId)
        {
            return GetImage(photoId, "Photo_RetrieveAdminThumbnailByPhotoID");
        }

        /// <summary>
        /// Retrieves the admin fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public byte[] RetrieveAdminFullsizeByPhotoId(int photoId)
        {
            return GetImage(photoId, "Photo_RetrieveAdminImageByPhotoID");
        }

        /// <summary>
        /// Retrieves the fullsize by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public byte[] RetrieveFullsizeByPhotoId(int photoId)
        {
            return GetImage(photoId, "Photo_RetrieveDisplayByPhotoID");
        }

        /// <summary>
        /// Retrieves the thumbnail by photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public byte[] RetrieveThumbnailByPhotoId(int photoId)
        {
            return GetImage(photoId, "Photo_RetrieveThumbnailByPhotoID");
        }

        /// <summary>
        /// Gets a random photo from the photo table
        /// </summary>
        /// <returns></returns>
        public Photo RetrieveRandomPhoto()
        {
            Photo photo = new Photo();

           SqlDataReader reader = _database.Reader("Photo_RetreiveRandomPhoto");
            List<Photo> photos = PopulatePhotos(reader);

            if (photos.Count > 0)
            {
                photo = photos[0];
            }

            return photo;
        }

        /// <summary>
        /// Gets one image by photo Id
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public byte[] GetImage(int photoId, string procedure)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@PhotoID",SqlDbType.Int,4,photoId)
			};

            object dbImage = _database.Scalar<object>(procedure, parameters);
            byte[] image = new byte[128];

            //check for image bits
            if (dbImage != DBNull.Value)
            {
                image = (byte[])dbImage;
            }

            return image;
        }

        /// <summary>
        /// populates a single photo class
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static Photo GetSingleItem(IDataReader reader)
        {
            Photo photo = new Photo();

            using (reader)
            {
                while (reader.Read())
                {
                    photo.PhotoID = Convert.ToInt32(reader["PhotoID"]);
                    photo.Title = Convert.ToString(reader["Title"]);
                    photo.Description = Convert.ToString(reader["Description"]);
                    photo.DateTaken = SetDateTaken(reader);
                    photo.Profile = EnumParse<PhotoProfile>.Parse(reader["Profile"].ToString());
                }
            }

            return photo;
        }


        /// <summary>
        /// Sets the dateTaken, a helper method to deal with null dates
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DateTime? SetDateTaken(IDataRecord reader)
        {
            DateTime? dateTime = null;

            if (reader["DateTaken"] != DBNull.Value)
            {
                dateTime = Convert.ToDateTime(reader["DateTaken"]);
            }

            return dateTime;
        }

        /// <summary>
        /// Moves a collection of photos to a new gallery
        /// </summary>
        /// <param name="photos">The photos.</param>
        public void MovePhotosToNewGallery(List<Photo> photos)
        {
            foreach (Photo item in photos)
            {
                MovePhotoToNewGallery(item);
            }
        }

        /// <summary>
        /// Moves a photo to a new gallery
        /// </summary>
        /// <param name="photo">The photo.</param>
        public void MovePhotoToNewGallery(Photo photo)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@PhotoID",SqlDbType.Int,4,photo.PhotoID)
			};

            _database.NonQuery("Photo_MovePhotoToNewGallery", parameters);
        }

        /// <summary>
        /// Retrieves all photos in a gallery
        /// </summary>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public List<Photo> RetreivePhotosByGalleryId(int galleryId)
        {
            SqlParameter[] parameters = 
			{
                _database.MakeParameter("@GalleryId",SqlDbType.Int,4,galleryId)
			};

            SqlDataReader reader = _database.Reader("Photo_RetrievePhotosByGalleryId", parameters);
            List<Photo> photos = PopulatePhotos(reader);

            return photos;
        }

        /// <summary>
        /// Gets all photos a user has uploaded
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserID(int userID)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@UserID",SqlDbType.Int,4,userID)
			};

            SqlDataReader reader = _database.Reader("Photo_RetrieveAllPhotosByUserId", parameters);
            List<Photo> photos = PopulatePhotos(reader);
            return photos;
        }

        /// <summary>
        /// Get's all photos by userId and Galleryid
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="galleryId">The gallery id.</param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@UserID",SqlDbType.Int,4,userID),
                _database.MakeParameter("@GalleryId",SqlDbType.Int,4,galleryId)
			};

            SqlDataReader reader = _database.Reader("Photo_RetrieveAllPhotosByUserIdAndGalleryId", parameters);
            List<Photo> photos = PopulatePhotos(reader);
            return photos;
        }

        /// <summary>
        /// Get's the orginal photo uploaded by photo id
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns></returns>
        public byte[] RetrieveOriginalByPhotoId(int photoId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@PhotoID",SqlDbType.Int,4,photoId),

			};

            byte[] image = _database.Scalar<byte[]>("Photo_RetrieveOriginalByPhotoID", parameters);
            return image;
        }

        /// <summary>
        /// Gets a random photo by user id
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int RetrieveRandomPhotoId(int userId)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@UserId",SqlDbType.Int,4,userId),

			};

            int photoId = _database.Scalar<int>("Photo_SelectRandomImageByUserID", parameters);
            return photoId;
        }

        /// <summary>
        /// Gets the last photo added
        /// </summary>
        /// <returns></returns>
        public Photo RetrieveLatestPhoto()
        {
            SqlDataReader reader = _database.Reader("Photo_RetrieveLatestPhoto");
            Photo _photo = GetSingleItem(reader);
            return _photo;
        }

        /// <summary>
        /// Deletes a collection of Photos
        /// </summary>
        /// <param name="photos">Collection photos to be deleted</param>
        public void Delete(List<Photo> photos)
        {
            foreach (Photo item in photos)
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Inserts Photo into the Photos Table
        /// </summary>
        /// <param name="photo">A new populated photo.</param>
        /// <param name="galleryPhoto">The gallery photo.</param>
        /// <param name="adminPhotos">Photos to be used in admin section</param>
        /// <returns>Insert Count</returns>
        public int Insert(Photo photo, GalleryPhoto galleryPhoto, GalleryPhoto adminPhotos)
        {
            SqlParameter[] parameters = 
			{
					_database.MakeParameter("@Title",SqlDbType.NVarChar,100,photo.Title),
                    _database.MakeParameter("@GalleryId",SqlDbType.Int,4,photo.GalleryId),
					_database.MakeParameter("@Description",SqlDbType.NVarChar,255,photo.Description),
					_database.MakeParameter("@DateTaken",SqlDbType.DateTime,8,photo.DateTaken),
					_database.MakeParameter("@OriginalImage",SqlDbType.Image, galleryPhoto.OriginalImage.Length, galleryPhoto.OriginalImage),
					_database.MakeParameter("@DisplayImage",SqlDbType.Image, galleryPhoto.DisplayImage.Length, galleryPhoto.DisplayImage),
					_database.MakeParameter("@ThumbnailImage",SqlDbType.Image, galleryPhoto.ThumbnailImage.Length, galleryPhoto.ThumbnailImage),
                    _database.MakeParameter("@AdminThumbnail",SqlDbType.Image, adminPhotos.ThumbnailImage.Length, adminPhotos.ThumbnailImage),
                    _database.MakeParameter("@AdminFullsizeImage",SqlDbType.Image, adminPhotos.DisplayImage.Length, adminPhotos.DisplayImage),
                    _database.MakeParameter("@Profile",SqlDbType.NVarChar, 50, photo.Profile)

			};

            try
            {
                _database.NonQuery("Photo_Insert", parameters);
            }
            catch (Exception)
            {
                
                throw;
            }


            return 1;
        }


        /// <summary>
        /// Updates the Photo table by the primary key, if the Photo is dirty then an update will occur
        /// </summary>
        /// <param name="photo">a populated photo</param>
        /// <returns>update count</returns>
        public int UpdateMetaData(Photo photo)
        {
            SqlParameter[] parameters = 
				{
					_database.MakeParameter("@PhotoID",SqlDbType.Int,4,photo.PhotoID),
					_database.MakeParameter("@Title",SqlDbType.NVarChar,100,photo.Title),
					_database.MakeParameter("@Description",SqlDbType.NVarChar,255,photo.Description),
					_database.MakeParameter("@DateTaken",SqlDbType.DateTime,8,photo.DateTaken)
				};

            int updateCount = _database.NonQuery("Photo_UpdateMetaData", parameters);

            return updateCount;
        }

        /// <summary>
        /// Updates the photos in the database
        /// </summary>
        /// <param name="photoID">Id of photo row</param>
        /// <param name="photos">the class containing the resized/new images</param>
        /// <returns></returns>
        public int UpdateImages(int photoID, GalleryPhoto photos)
        {
            SqlParameter[] parameters = 
				{
					_database.MakeParameter("@PhotoID",SqlDbType.Int,4,photoID),
                    _database.MakeParameter("@OriginalImage",SqlDbType.Image, photos.OriginalImage.Length, photos.OriginalImage),
                    _database.MakeParameter("@DisplayImage",SqlDbType.Image, photos.DisplayImage.Length, photos.DisplayImage),
                    _database.MakeParameter("@ThumbnailImage",SqlDbType.Image, photos.ThumbnailImage.Length, photos.ThumbnailImage)

				};

            int updateCount = _database.NonQuery("Photo_ImagesUpdate", parameters);
            return updateCount;
        }



        /// <summary>
        /// Delete a Photo by the primary key
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public int Delete(Photo photo)
        {
            SqlParameter[] parameters = 
			{
				_database.MakeParameter("@PhotoID",SqlDbType.Int,4, photo.PhotoID),

			};

            return _database.NonQuery("Photo_Delete", parameters);
        }

        /// <summary>
        /// Retrieves the first photo from the reader
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal static Photo Populate(SqlDataReader reader)
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
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static Photo GetItem(IDataRecord reader)
        {
            Photo photo = new Photo
                              {
                                  PhotoID = Convert.ToInt32(reader["PhotoID"]),
                                  Title = Convert.ToString(reader["Title"]),
                                  Description = Convert.ToString(reader["Description"]),
                                  DateTaken = SetDateTaken(reader),
                                  Profile = EnumParse<PhotoProfile>.Parse(reader["Profile"].ToString())
                              };

            return photo;
        }


        /// <summary>
        /// Retrieves a collection of photos from a reader
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal static List<Photo> PopulatePhotos(SqlDataReader reader)
        {
            List<Photo> photos = new List<Photo>();

            using (reader)
            {
                while (reader.Read())
                {
                    Photo _photo = GetItem(reader);
                    photos.Add(_photo);
                }
            }

            return photos;
        }


    }
}
