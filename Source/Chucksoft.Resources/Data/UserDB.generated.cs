using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Chucksoft.Entities;
using System.Data.Common;

namespace Chucksoft.Resources.Data
{
	internal partial class UserDB
	{
	    /// <summary>
        /// Inserts User into the Users Table
        /// </summary>
        /// <param name="user">A new populated user.</param>
        /// <returns>Insert Count</returns>
        public int Insert(User user, string commandText)
        {
			DbParameter[] parameters = 
			{
					DbClient.MakeParameter("@Email",DbType.String,50,user.Email),
					DbClient.MakeParameter("@Password",DbType.String,150,user.Password),
					DbClient.MakeParameter("@FirstName",DbType.String,50,user.FirstName),
                    DbClient.MakeParameter("@LastName",DbType.String,50,user.LastName),
					DbClient.MakeParameter("@Access",DbType.Byte,1,user.Access),
					DbClient.MakeParameter("@Website",DbType.String,200,user.Website)

			};

            return DbClient.NonQuery(commandText, parameters);

        }

	
	    /// <summary>
        /// Updates the User table by the primary key, if the User is dirty then an update will occur
        /// </summary>
        /// <param name="user">a populated user</param>
        /// <returns>update count</returns>
        public int Update(User user, string commandText)
        {
            int updateCount = 0;

            if(user.IsDirty())
            {
				DbParameter[] parameters = 
				{
					DbClient.MakeParameter("@UserId",DbType.Int32,4,user.UserId),
					DbClient.MakeParameter("@Email",DbType.String,50,user.Email),
					DbClient.MakeParameter("@Password",DbType.String,150,user.Password),
					DbClient.MakeParameter("@FirstName",DbType.String,50,user.FirstName),
                    DbClient.MakeParameter("@LastName",DbType.String,50,user.LastName),
					DbClient.MakeParameter("@Access",DbType.Byte,1,user.Access),
					DbClient.MakeParameter("@Website",DbType.String,200,user.Website)

				};

                updateCount = DbClient.NonQuery(commandText, parameters);

            }

            return updateCount;
        }
        
        /// <summary>
        /// Delete a User by the primary key
        /// </summary>
        /// <param name="user"></param>
        public int Delete(User user, string commandText)
        {
			DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserId",DbType.Int32,4,user.UserId),

			};

            return DbClient.NonQuery(commandText, parameters);
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal User Populate(DbDataReader reader)
        {
            User user = new User();

            using (reader)
            {
                while (reader.Read())
                {
                    user = GetItem(reader);                    
                }
            }

            return user;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private User GetItem(IDataRecord reader)
        {
            User user = new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Email = Convert.ToString(reader["Email"]),
                                Password = Convert.ToString(reader["Password"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                Access = Convert.ToByte(reader["Access"]),
                                Website = Convert.ToString(reader["Website"]),
                                ServiceKey = new Guid(reader["ServiceKey"].ToString())
                            };


            return user;
        }

        /// <summary>
        /// Populates the users.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal List<User> PopulateUsers(DbDataReader reader)
        {
            List<User> users = new List<User>();

            using (reader)
            {
                while (reader.Read())
                {
                    User user = GetItem(reader);
                    users.Add(user);
                }
            }

            return users;
       }
    }
}
