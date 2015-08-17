using System;
using Chucksoft.Entities;
using System.Collections.Generic;

namespace Chucksoft.Resources
{
    public interface IUser
    {
        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int Delete(User user);

        /// <summary>
        /// Deletes the specified users.
        /// </summary>
        /// <param name="users">The users.</param>
        void Delete(List<User> users);

        /// <summary>
        /// Emails the address count.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        int EmailAddressCount(string emailAddress);

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int Insert(User user);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns></returns>
        List<User> RetrieveAllUsers();

        /// <summary>
        /// Retrieves the user by service key.
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns></returns>
        User RetrieveUserByServiceKey(Guid serviceKey);

        /// <summary>
        /// Retrieves the user by user ID.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        User RetrieveUserByUserID(int userId);

        /// <summary>
        /// Retrieves the user by username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        User RetrieveUserByUsernameAndPassword(string username, string password);

        /// <summary>
        /// Retrieves the user count by username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        int RetrieveUserCountByUsernameAndPassword(string username, string password);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int Update(User user);
    }
}
