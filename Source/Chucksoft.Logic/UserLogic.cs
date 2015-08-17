using System;
using System.Collections.Generic;
using Chucksoft.Entities;
using Chucksoft.Entities.Interfaces.Resources;
using Chucksoft.Resources.Database;
using Conway.Security.Cryptography;

namespace Chucksoft.Logic
{
    public class UserLogic
    {
        private readonly IUserDb _resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryLogic"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public UserLogic(IUserDb resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryLogic"/> class.
        /// </summary>
        public UserLogic() : this(new UserDb()) { }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Add(User user)
        {
            _resource.Insert(user);
        }

        /// <summary>
        /// Delete a collection of users.
        /// </summary>
        /// <param name="users">The users.</param>
        public void Delete(List<User> users)
        {
            _resource.Delete(users);
        }

        /// <summary>
        /// Delete a single user
        /// </summary>
        /// <param name="user">The user.</param>
        public void Delete(User user)
        {

            _resource.Delete(user);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns></returns>
        public List<User> RetrieveAllUsers()
        {
            List<User> users = _resource.RetrieveAllUsers();

            return users;
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(User user)
        {
           _resource.Update(user);
        }

        /// <summary>
        /// Retrieves the user by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public User RetrieveUserByToken(string token)
        {
            bool isValidToken = IsValidToken(token);
            User user = new User(); 

            //if valid token, proceed to use it.
            if (isValidToken)
            {
                string[] tokenSegments = DecryptToken(token);

                Guid serviceKey = new Guid(tokenSegments[1]);
                user = _resource.RetrieveUserByServiceKey(serviceKey);
            }

            return user;
        }

        /// <summary>
        /// Determines whether [is valid token] [the specified token].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid token] [the specified token]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidToken(string token)
        {
            bool isValidToken = false;

            try
            {
                string[] tokenSegments = DecryptToken(token);
                long ticks = Convert.ToInt64(tokenSegments[0]);

                DateTime time = new DateTime(ticks);

                if (time < DateTime.UtcNow.AddHours(5))
                {
                    isValidToken = true;
                }
            }
            catch
            {
               // will catch encryption errors...
                isValidToken = false;
            }

            return isValidToken;
        }

        /// <summary>
        /// Decrypts the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private static string[] DecryptToken(string token)
        {
            string unencryptedToken = RijndaelCryptography.Decrypt(token);
            return unencryptedToken.Split('|');
        }

        /// <summary>
        /// Generates the user token.
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns></returns>
        public string GenerateUserToken(Guid serviceKey)
        {
            bool isValidUser = IsValidUser(serviceKey);
            string encryptedToken = string.Empty;

            if (isValidUser)
            {
                //Add 5 hours to the current UTC
                DateTime time = DateTime.UtcNow.AddHours(5);

                //concate and encrypt the datetime and service key
                string unencryptedToken = time.Ticks + "|" + serviceKey;
                encryptedToken = RijndaelCryptography.Encrypt(unencryptedToken);
            }

            return encryptedToken;
        }

        /// <summary>
        /// Determines whether [is valid user] [the specified service key].
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid user] [the specified service key]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidUser(Guid serviceKey)
        {
            User user = _resource.RetrieveUserByServiceKey(serviceKey);
            bool isValidUser = true;

            //check for valid user/ServiceKey
            if(user.UserId < 1)
            {
                isValidUser = false; 
            }

            return isValidUser;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool AuthenticateUser(string username, string password)
        {
            int count = _resource.RetrieveUserCountByUsernameAndPassword(username, password);

            bool isValidUser = (count == 1);
            return isValidUser;
        }

        /// <summary>
        /// Retrieves the user by username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public User RetrieveUserByUsernameAndPassword(string username, string password)
        {
            User user = _resource.RetrieveUserByUsernameAndPassword(username, password);
            return user;
        }

        /// <summary>
        /// Retrieves the user by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A fully populated User object</returns>
        public User RetreiveUserByUserId(int userId)
        {
            User user = _resource.RetrieveUserByUserID(userId);
            return user;
        }

        /// <summary>
        /// Determines whether [is unique email] [the specified email address].
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>
        /// 	<c>true</c> if [is unique email] [the specified email address]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUniqueEmail(string emailAddress)
        {
            int emailCount = _resource.EmailAddressCount(emailAddress);

            bool isUniqueEmailAddress = (emailCount == 0);
            return isUniqueEmailAddress;
        }
    }
}