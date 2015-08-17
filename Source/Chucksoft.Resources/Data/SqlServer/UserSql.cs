using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlServer
{
    public class UserSql : IUser
    {
        UserDB userDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSql"/> class.
        /// </summary>
        public UserSql()
        {
            userDb = new UserDB();
            userDb.DbClient = new SqlServerClient();
        }

        /// <summary>
        /// Inserts User into the Users Table
        /// </summary>
        /// <param name="user">A new populated user.</param>
        /// <returns>Insert Count</returns>
        public int Insert(User user)
        {
           return userDb.Insert(user, "User_Insert");
        }

        /// <summary>
        /// Updates the User table by the primary key, if the User is dirty then an update will occur
        /// </summary>
        /// <param name="user">a populated user</param>
        /// <returns>update count</returns>
        public int Update(User user)
        {
            int updateCount = userDb.Update(user, "User_Update");
            return updateCount;
        }

        string deleteProcedure = "User_Delete";

        /// <summary>
        /// Delete a User by the primary key
        /// </summary>
        /// <param name="user"></param>
        public int Delete(User user)
        {
            return userDb.Delete(user, deleteProcedure);
        }

        /// <summary>
        /// Delete a collection of users at once
        /// </summary>
        /// <param name="users">A collection of users</param>
        public void Delete(List<User> users)
        {
            foreach (User user in users)
            {
                userDb.Delete(user, deleteProcedure);
            }
        }

        /// <summary>
        /// checks the username and password exist in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int RetrieveUserCountByUsernameAndPassword(string username, string password)
        {
            int count = userDb.RetrieveUserCountByUsernameAndPassword(username, password, "User_RetrieveUserCountByUsernameAndPassword");
            return count;
        }

        /// <summary>
        /// Gets the user by their serviceKey
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByServiceKey(Guid serviceKey)
        {
            User user = userDb.RetrieveUserByServiceKey(serviceKey, "User_SelectByServiceKey");
            return user;
        }

        /// <summary>
        /// Makes sure the email address is not already being used
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int EmailAddressCount(string emailAddress)
        {
            int count = userDb.EmailAddressCount(emailAddress, "User_EmailAddressCount");
            return count;
        }

        /// <summary>
        /// Retrieve the user by the id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByUserID(int userId)
        {
            User user = userDb.RetrieveUserByUserID(userId, "User_RetrieveUserByUserID");
            return user;
        }

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<User> RetrieveAllUsers()
        {
           List<User> users = userDb.RetrieveAllUsers("User_RetrieveAll");
            return users;
        }

        /// <summary>
        /// Get the user by the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public User RetrieveUserByUsernameAndPassword(string username, string password)
        {
            User user = userDb.RetrieveUserByUsernameAndPassword(username, password, "User_RetrieveUserByUsernameAndPassword");
            return user;
        }
    }
}
