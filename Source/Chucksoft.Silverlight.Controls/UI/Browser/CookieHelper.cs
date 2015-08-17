using System;
using System.Windows.Browser;

namespace Chucksoft.Silverlight.Controls.UI.Browser
{
    public class CookieHelper
    {
        /// <summary>
        /// sets a persistent cookie with huge expiration date
        /// </summary>
        /// <param name="key">the cookie key</param>
        ///  <param name="value">the cookie value</param>
        public static void SetCookie(string key, string value)
        {
            DateTime expiration = DateTime.UtcNow + TimeSpan.FromDays(2000);
            string cookie = String.Format("{0}={1};expires={2}", key, value, expiration.ToString("R"));
            HtmlPage.Document.SetProperty("cookie", cookie);

        }

        /// <summary>
        /// Retrieves an existing cookie
        /// </summary>
        /// <param name="key">cookie key</param>
        /// <returns>null if the cookie does not exist, otherwise the cookie value</returns>
        public static string GetCookie(string key)
        {
            string[] cookies = HtmlPage.Document.Cookies.Split(';');
            key += '=';

            foreach (string cookie in cookies)
            {
                if (cookie.Contains(key))
                {
                    string cookieStr = cookie.Trim();
                    string[] values = cookieStr.Split(new[] {key}, StringSplitOptions.None);

                    //Hack!
                    string[] vals = values[1].Split('&');
                    return vals[0];
                }
            }

            return null;

        }

        /// <summary>
        /// Deletes a specified cookie by setting its value to empty and expiration to -1 days
        /// </summary>
        /// <param name="key">the cookie key to delete</param>
        public static void DeleteCookie(string key)
        {
            DateTime expiration = DateTime.UtcNow - TimeSpan.FromDays(1);
            string cookie = String.Format("{0}=;expires={1}", key, expiration.ToString("R"));
            HtmlPage.Document.SetProperty("cookie", cookie);
        }

        /// <summary>
        /// Retrieves the cookie value.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string RetrieveCookieValue(string cookieName, string key)
        {
            string cookie = GetCookie(cookieName);
            string cookieValue = string.Empty;

            if (!string.IsNullOrEmpty(cookie))
            {
                string cookieKey = key + "=";


                string[] cookieSegments = cookie.Split('&');

                foreach (string segment in cookieSegments)
                {
                    if (segment.StartsWith(cookieKey, StringComparison.InvariantCultureIgnoreCase))
                    {
                        cookieValue = segment.Replace(cookieKey, "");
                    }
                }
            }
            return cookieValue;
        }
    }
}