using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Chucksoft.Entities.Interfaces.Resources
{
    public interface IUserDb
    {
        /// <summary>
        /// Inserts User into the Users Table
        /// </summary>
        /// <param name="user">A new populated user.</param>
        /// <returns>Insert Count</returns>
        int Insert(User user);

        /// <summary>
        /// Updates the User table by the primary key, if the User is dirty then an update will occur
        /// </summary>
        /// <param name="user">a populated user</param>
        /// <returns>update count</returns>
        int Update(User user);

        /// <summary>
        /// Delete a User by the primary key
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int Delete(User user);

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        User Populate(DbDataReader reader);

        /// <summary>
        /// Populates the users.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        List<User> PopulateUsers(DbDataReader reader);

        /// <summary>
        /// checks the username and password exist in the database
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        int RetrieveUserCountByUsernameAndPassword(string username, string password);

        /// <summary>
        /// Gets the user by their serviceKey
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns></returns>
        User RetrieveUserByServiceKey(Guid serviceKey);

        /// <summary>
        /// Makes sure the email address is not already being used
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        int EmailAddressCount(string emailAddress);

        /// <summary>
        /// Retrieve the user by the id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        User RetrieveUserByUserID(int userId);

        /// <summary>
        /// Gets all the users from the database
        /// </summary>
        /// <returns></returns>
        List<User> RetrieveAllUsers();

        /// <summary>
        /// Get the user by the username and password
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        User RetrieveUserByUsernameAndPassword(string username, string password);

        /// <summary>
        /// Deletes the specified users.
        /// </summary>
        /// <param name="users">The users.</param>
        void Delete(List<User> users);
    }
}