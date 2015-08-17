using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Chucksoft.Entities;
using Chucksoft.Entities.Interfaces.Resources;

namespace Chucksoft.Resources.Database
{
    public class UserDb : IUserDb
    {
        private readonly ISqlServer _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDb"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public UserDb(ISqlServer database)
        {
            _database = database;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDb"/> class.
        /// </summary>
        public UserDb() : this(new SqlServer()) {}

        /// <summary>
        /// Inserts User into the Users Table
        /// </summary>
        /// <param name="user">A new populated user.</param>
        /// <returns>Insert Count</returns>
        public int Insert(User user)
        {
            SqlParameter[] parameters = 
			{
					_database.MakeParameter("@Email",SqlDbType.NVarChar, 50,user.Email),
					_database.MakeParameter("@Password",SqlDbType.NVarChar, 150,user.Password),
					_database.MakeParameter("@FirstName",SqlDbType.NVarChar, 50,user.FirstName),
                    _database.MakeParameter("@LastName",SqlDbType.NVarChar, 50,user.LastName),
					_database.MakeParameter("@Access",SqlDbType.TinyInt, 1,user.Access),
					_database.MakeParameter("@Website",SqlDbType.NVarChar, 200,user.Website)
			};

            return _database.NonQuery("User_Insert", parameters);
        }


        /// <summary>
        /// Updates the User table by the primary key, if the User is dirty then an update will occur
        /// </summary>
        /// <param name="user">a populated user</param>
        /// <returns>update count</returns>
        public int Update(User user)
        {
            SqlParameter[] parameters = 
				{
					_database.MakeParameter("@UserId",SqlDbType.Int, 4, user.UserId),
					_database.MakeParameter("@Email",SqlDbType.NVarChar, 50, user.Email),
					_database.MakeParameter("@Password",SqlDbType.NVarChar, 150, user.Password),
					_database.MakeParameter("@FirstName",SqlDbType.NVarChar, 50, user.FirstName),
                    _database. MakeParameter("@LastName",SqlDbType.NVarChar, 50, user.LastName),
					_database.MakeParameter("@Access",SqlDbType.TinyInt, 1, user.Access),
					_database.MakeParameter("@Website",SqlDbType.NVarChar, 200, user.Website)
				};

            int updateCount = _database.NonQuery("User_Delete", parameters);

            return updateCount;
        }

        /// <summary>
        /// Deletes the specified users.
        /// </summary>
        /// <param name="users">The users.</param>
        public void Delete(List<User> users)
        {
            for (int index = 0; index < users.Count; index++)
            {
                Delete(users[index]);
            }
        }

        /// <summary>
        /// Delete a User by the primary key
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int Delete(User user)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@UserId", SqlDbType.Int, 4, user.UserId)
			};

            return _database.NonQuery("User_Delete", parameters);
        }


        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public User Populate(DbDataReader reader)
        {
            User user = new User();

            using (reader)
            {
                if(reader.Read())
                {
                   user = GetItem(reader);
                }
            }

            return user;
        }


        /// <summary>
        /// Populates the users.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public List<User> PopulateUsers(DbDataReader reader) 
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

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static User GetItem(IDataRecord reader)
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
        /// checks the username and password exist in the database
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public int RetrieveUserCountByUsernameAndPassword(string username, string password)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@Username", SqlDbType.NVarChar, 50, username),
                _database.MakeParameter("@Password", SqlDbType.NVarChar, 150, password)
			};

            int count = _database.Scalar<int>("User_RetrieveUserCountByUsernameAndPassword", parameters);
            return count;
        }

        /// <summary>
        /// Gets the user by their serviceKey
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns></returns>
        public User RetrieveUserByServiceKey(Guid serviceKey)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@ServiceKey", SqlDbType.UniqueIdentifier, 16, serviceKey)
			};

            SqlDataReader reader = _database.Reader("User_SelectByServiceKey", parameters);
            User user = Populate(reader);

            return user;
        }

        /// <summary>
        /// Makes sure the email address is not already being used
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public int EmailAddressCount(string emailAddress)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@Email", SqlDbType.NVarChar, 50, emailAddress)
			};

            int count = _database.Scalar<int>("User_EmailAddressCount", parameters);
            return count;
        }

        /// <summary>
        /// Retrieve the user by the id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public User RetrieveUserByUserID(int userId)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@UserId", SqlDbType.Int, 4, userId)
			};

            SqlDataReader reader = _database.Reader("User_RetrieveUserByUserID", parameters);
            User user = Populate(reader);

            return user;
        }

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> RetrieveAllUsers()
        {
            SqlDataReader reader = _database.Reader("User_RetrieveAll");
            List<User> users = PopulateUsers(reader);

            return users;
        }

        /// <summary>
        /// Get the user by the username and password
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public User RetrieveUserByUsernameAndPassword(string username, string password)
        {
            SqlParameter[] parameters = 
			{ 
				_database.MakeParameter("@Username", SqlDbType.NVarChar, 50, username),
                _database.MakeParameter("@Password", SqlDbType.NVarChar, 150, password)
			};

            SqlDataReader reader = _database.Reader("User_RetrieveUserByUsernameAndPassword", parameters);
            User user = Populate(reader);

            return user;
        }
    }
}
