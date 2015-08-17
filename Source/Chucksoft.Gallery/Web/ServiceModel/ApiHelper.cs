using Chucksoft.Entities.Exceptions;
using Chucksoft.Logic;

namespace Chucksoft.Web.ServiceModel
{
    public static class ApiHelper
    {
        /// <summary>
        /// Valids the token.
        /// </summary>
        /// <param name="token">The token.</param>
        public static void ValidToken(string token)
        {
            bool isTokenValid = UserLogic.IsValidToken(token);

            //Check for the validity of the token.
            if (!isTokenValid)
            {
                throw new InvalidTokenException();
            }
        }
    }
}