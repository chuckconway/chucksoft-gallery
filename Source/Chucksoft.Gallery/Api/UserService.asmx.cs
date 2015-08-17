using System;
using System.ComponentModel;
using System.Web.Services;
using Chucksoft.Entities;
using Chucksoft.Logic;
using Conway.Security;

namespace Chucksoft.Api
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://chucksoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : WebService
    {

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns></returns>
        [WebMethod]
        public string GetToken(Guid serviceKey)
        {
            string userToken = new UserLogic().GenerateUserToken(serviceKey);
            return userToken;

        }

        /// <summary>
        /// Gets the token by username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [WebMethod]
        public string GetTokenByUsernameAndPassword(string username, string password)
        {
            string hashedPassword = SimpleHash.ComputeHash(password, SimpleHash.Algorithm.SHA256, new byte[8]);
            User user = new UserLogic().RetrieveUserByUsernameAndPassword(username, hashedPassword);

            Guid serviceKey = user.ServiceKey;
            string token = new UserLogic().GenerateUserToken(serviceKey);

            return token;
        }
    }
}
