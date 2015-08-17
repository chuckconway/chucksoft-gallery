using System.Security.Principal;
namespace Chucksoft.Entities
{
    public class GalleryIdentity : IIdentity
    {
        #region IIdentity Members

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <value></value>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType
        {
            get { return "Forms"; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <value></value>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated { get; set;}


        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name { get; set;}

        #endregion
    }
}
