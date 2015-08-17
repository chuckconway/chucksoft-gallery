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
        /// Gets or sets the db client.
        /// </summary>
        /// <value>The db client.</value>
        public DbClient DbClient { get; set; }

        /// <summary>
        /// checks the username and password exist in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int RetrieveUserCountByUsernameAndPassword(string username, string password, string commandText)
		{
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@Username",DbType.String,150,username),
				DbClient.MakeParameter("@Password",DbType.String,150,password)
			};

            int count = (int)DbClient.Scalar(commandText, parameters);
		    return count;
		}

        /// <summary>
        /// Gets the user by their serviceKey
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByServiceKey(Guid serviceKey, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@ServiceKey", DbType.Guid,6,serviceKey)

			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<User> users = PopulateUsers(reader);
            User user = GetUser(users);

            return user;
        }

        /// <summary>
        /// Delete a collection of users at once
        /// </summary>
        /// <param name="users">A collection of users</param>
        public void Delete(List<User> users, string commandText)
        {
            foreach (User user in users)
            {
                Delete(user, commandText);
            }
        }

        /// <summary>
        /// Makes sure the email address is not already being used
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int EmailAddressCount(string emailAddress, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@EmailAddress",DbType.String,150,emailAddress)
			};

            int count = (int)DbClient.Scalar(commandText, parameters);

		    return count;
        }

        /// <summary>
        /// Retrieve the user by the id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByUserID(int userId, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@UserId",DbType.Int32,4 ,userId)
			};


            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<User> users = PopulateUsers(reader);
            User user = GetUser(users);

            return user;
        }

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<User> RetrieveAllUsers(string commandText)
        {
            DbDataReader reader = DbClient.Reader(commandText);
            List<User> users = PopulateUsers(reader);

            return users;
        }

        /// <summary>
        /// Get the user by the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByUsernameAndPassword(string username, string password, string commandText)
        {
            DbParameter[] parameters = 
			{
				DbClient.MakeParameter("@Username",DbType.String,150,username),
				DbClient.MakeParameter("@Password",DbType.String,150,password)
			};

            DbDataReader reader = DbClient.Reader(commandText, parameters);
            List<User> users = PopulateUsers(reader);
            User user = GetUser(users);

            return user;
        }


        /// <summary>
        /// Gets the first user in the collection
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private User GetUser(IList<User> users)
        {
            User user = new User();

            if(users.Count > 0)
            {
                user = users[0];
            }

            return user;
        }
    }
} 