using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;

namespace Chucksoft.Resources.Data.SqlCe
{
    public class UserSqlCe : IUser
    {
         UserDB userDb;

         /// <summary>
         /// Initializes a new instance of the <see cref="UserSqlCe"/> class.
         /// </summary>
        public UserSqlCe()
        {
            userDb = new UserDB();
            userDb.DbClient = new SqlCeClient();
        }

        /// <summary>
        /// Inserts User into the Users Table
        /// </summary>
        /// <param name="user">A new populated user.</param>
        /// <returns>Insert Count</returns>
        public int Insert(User user)
        {
            const string sql = "INSERT INTO User (Email, Password, FirstName, LastName, Access, Website)  VALUES (@Email, @Password, @FirstName, @LastName, @Access, @Website)";
            return userDb.Insert(user, sql);
        }

        /// <summary>
        /// Updates the User table by the primary key, if the User is dirty then an update will occur
        /// </summary>
        /// <param name="user">a populated user</param>
        /// <returns>update count</returns>
        public int Update(User user)
        {
            const string sql = "Update User	SET	Email = @Email, Password = @Password, FirstName = @FirstName, LastName = @LastName, Access = @Access,   Website = @Website Where UserId = @UserId ";
            int updateCount = userDb.Update(user, sql);
            return updateCount;
        }

        string deleteProcedure = "Update User SET Deleted = 1 Where UserId = @UserId ";

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
            const string sql = "Select count(*) From User Where Email = @Username AND Password = @Password AND Deleted <> 1";

            int count = userDb.RetrieveUserCountByUsernameAndPassword(username, password, sql);
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
            const string sql = "Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey From User 	Where ServiceKey = @ServiceKey ";

            User user = userDb.RetrieveUserByServiceKey(serviceKey, sql);
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
            const string sql = "Select count(*) From User Where Email = @EmailAddress";

            int count = userDb.EmailAddressCount(emailAddress, sql);
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
            const string sql = "Select UserId, Email, Password, FirstName, LastName, Access, ServiceKey, Website From User 	Where UserId = @UserId ";

            User user = userDb.RetrieveUserByUserID(userId, sql);
            return user;
        }

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<User> RetrieveAllUsers()
        {
            const string sql = "Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey From User Where Deleted <> 1";

            List<User> users = userDb.RetrieveAllUsers(sql);
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
            const string sql = "Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey From User 	Where Email = @Username AND Password = @Password And Deleted <> 1";

            User user = userDb.RetrieveUserByUsernameAndPassword(username, password, sql);
            return user;
        }
    }
}
