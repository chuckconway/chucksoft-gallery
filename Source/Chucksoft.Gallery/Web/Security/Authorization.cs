using System;
using System.Text;
using System.Web;
using Chucksoft.Admin;
using Chucksoft.Logic;
using Chucksoft.Entities;
using Conway.Security.Cryptography;

namespace Chucksoft.Web.Security
{
    public class Authorization
    {
        private string username;
        private string password;
        private const string defaultKey = "x29uRqfMStaOPty7";
        // private const string defaultIV = "ZVuQgawmoCN5L0n0";

        /// <summary>
        /// Authenticateds the user.
        /// </summary>
        /// <returns></returns>
        public bool AuthenticatedUser()
        {
            bool isValidUser = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[AdminResources.AuthenticationCookieName];

            if (cookie != null)
            {
                cookie = DecryptCookie(cookie);

                username = cookie["Username"];
                password = cookie["Password"];

                username = Encoding.ASCII.GetString(Convert.FromBase64String(username));
                isValidUser = new  UserLogic().AuthenticateUser(username, password);
            }

            return isValidUser;
        }

        /// <summary>
        /// Sets the authentication cookie.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public static void SetAuthenticationCookie(string username, string password)
        {
            HttpCookie cookie = new HttpCookie(AdminResources.AuthenticationCookieName);
            cookie["Username"] = username;
            cookie["Password"] = password;

            SaveCookieOnClient(cookie, 14);
        }

        /// <summary>
        /// Retrieves the user.
        /// </summary>
        /// <returns></returns>
        public User RetrieveUser()
        {
            User user = new UserLogic().RetrieveUserByUsernameAndPassword(username, password);
            return user;
        }

        /// <summary>
        /// Saves the cookie on client.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        public static void SaveCookieOnClient(HttpCookie cookie)
        {
            SaveCookieOnClient(cookie, 0);
        }

        /// <summary>
        /// Encrypts and Saves cookie
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <param name="days">The days.</param>
        public static void SaveCookieOnClient(HttpCookie cookie, int days)
        {
            cookie = EncryptCookie(cookie);
            cookie.Domain = string.Format(".{0}", HttpContext.Current.Request.Url.Host);

            if (days > 0)
            {
                cookie.Expires = DateTime.Now.AddDays(days);
            }

            SetUnencryptedCookie(cookie);
        }

        /// <summary>
        /// Sets the unencrypted cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        public static void SetUnencryptedCookie(HttpCookie cookie)
        {
            HttpContext.Current.Request.Cookies.Remove(cookie.Name);
            HttpContext.Current.Request.Cookies.Add(cookie);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Encode Cookie values
        /// </summary>
        /// <param name="encryptCookie">The encrypt cookie.</param>
        /// <returns></returns>
        private static HttpCookie EncryptCookie(HttpCookie encryptCookie)
        {
            HttpCookie cookie = new HttpCookie(encryptCookie.Name)
                                    {
                                        Value = RijndaelCryptography.Encrypt(encryptCookie.Value, defaultKey),
                                        Domain = string.Format(".{0}", HttpContext.Current.Request.Url.Host)
                                    };

            return cookie;
        }

        /// <summary>
        /// Decrpyt Cookie values
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns></returns>
        private static HttpCookie DecryptCookie(HttpCookie cookie)
        {
            HttpCookie _cookie = new HttpCookie(cookie.Name);

            if (!string.IsNullOrEmpty(cookie.Value))
            {
                _cookie.Value = RijndaelCryptography.Decrypt(cookie.Value, defaultKey);
            }

            return _cookie;
        }
    }
}