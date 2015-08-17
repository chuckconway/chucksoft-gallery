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
        /// Gets or sets the db client.
        /// </summary>
        /// <value>The db client.</value>
        public DbClient DbClient { get; set; }

        /// <summary>
        /// Retrieves a random photo from a specified gallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public Photo RetrieveRandomPhotoByGalleryId(int galleryId, string commandText)
        {
            DbParameter[] parameters = 
			{
                DbClient.MakeParameter("@GalleryId",DbType.Int32,4,galleryId)
			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<Photo> photos = PopulatePhotos(reader);
            Photo photo = new Photo();

            if (photos.Count > 0)
            {
                photo = photos[0];
            }

            return photo;
        }

        /// <summary>
        /// Gets a random photo from the photo table
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public Photo RetrieveRandomPhoto(string commandText)
        {
            DbDataReader reader = DbClient.Reader(commandText);
            List<Photo> photos = PopulatePhotos(reader);
            Photo photo = new Photo();

            if(photos.Count > 0)
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
           DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photoId),

			};

           object dbImage = DbClient.Scalar(procedure, parameters);
            byte[] image = new byte[128];

            //check for image bits
            if(dbImage != DBNull.Value)
            {
                image = (byte[])dbImage;
            }

            return image;
        }

        /// <summary>
        /// populates a single photo class
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Photo GetSingleItem(IDataReader reader)
        {
            Photo photo = new Photo();

            using (reader)
            {
                while(reader.Read())
                {
                    photo.PhotoID = Convert.ToInt32(reader["PhotoID"]);
                    photo.Title = Convert.ToString(reader["Title"]);
                    photo.Description = Convert.ToString(reader["Description"]);
                    photo.DateTaken = SetDateTaken(reader);
                    photo.GalleryID = Convert.ToInt32(reader["GalleryID"]);
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
        private DateTime? SetDateTaken(IDataRecord reader)
        {
            DateTime? dateTime = null;

            if(reader["DateTaken"] != DBNull.Value)
            {
                dateTime = Convert.ToDateTime(reader["DateTaken"]);
            }

            return dateTime;
        }

        /// <summary>
        /// Moves a collection of photos to a new gallery
        /// </summary>
        /// <param name="photos"></param>
        public void MovePhotosToNewGallery(List<Photo> photos, string commandText)
        {
            foreach (Photo item in photos)
            {
                MovePhotoToNewGallery(item, commandText);
            }
        }

        /// <summary>
        /// Moves a photo to a new gallery
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="commandText"></param>
        public void MovePhotoToNewGallery(Photo photo, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photo.PhotoID),
                DbClient.MakeParameter("@GalleryId",DbType.Int32,4,photo.GalleryID)

			};

            DbClient.NonQuery(commandText, parameters);
        }

        /// <summary>
        /// Retrieves all photos in a gallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Photo> RetreivePhotosByGalleryId(int galleryId, string commandText)
        {
            DbParameter[] parameters = 
			{
                DbClient.MakeParameter("@GalleryId",DbType.Int32,4,galleryId)
			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<Photo> photos = PopulatePhotos(reader);

            return photos;
        }

        /// <summary>
        /// Gets all photos a user has uploaded
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserID(int userID, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserID",DbType.Int32,4,userID)
			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<Photo> photos = PopulatePhotos(reader);
            return photos;
        }

        /// <summary>
        /// Get's all photos by userId and Galleryid
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="galleryId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<Photo> RetrievePhotosByUserIDAndGalleryId(int userID, int galleryId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserID",DbType.Int32,4,userID),
                DbClient.MakeParameter("@GalleryId",DbType.Int32,4,galleryId)
			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<Photo> photos = PopulatePhotos(reader);
            return photos;
        }

        /// <summary>
        /// Get's the orginal photo uploaded by photo id
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public byte[] RetrieveOriginalByPhotoId(int photoId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@PhotoID",DbType.Int32,4,photoId),

			};

            byte[] image = (byte[])DbClient.Scalar(commandText, parameters);
            return image;
        }

        /// <summary>
        /// Gets a random photo by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int RetrieveRandomPhotoId(int userId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserId",DbType.Int32,4,userId),

			};

            int photoId = (int)DbClient.Scalar(commandText, parameters);
            return photoId;
        }

        /// <summary>
        /// Gets the last photo added
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public Photo RetrieveLatestPhoto(string commandText)
        {
            DbDataReader reader = DbClient.Reader(commandText);
            Photo photo = GetSingleItem(reader);
            return photo;
        }

        /// <summary>
        /// Deletes a collection of Photos
        /// </summary>
        /// <param name="photos">Collection photos to be deleted</param>
        public void Delete(List<Photo> photos, string commandText)
        {
            foreach (Photo item in photos)
            {
                Delete(item, commandText);                
            }
        } 
    }
}