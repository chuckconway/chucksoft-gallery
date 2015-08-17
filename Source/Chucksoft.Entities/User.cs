using System;
using System.Security.Principal;
using System.Xml.Serialization;

namespace Chucksoft.Entities
{
    [Serializable]
    public class User : IPrincipal
    {
        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.</returns>
        [XmlIgnore]
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return false;
        }
        
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>The access.</value>
        public byte Access { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the service key.
        /// </summary>
        /// <value>The service key.</value>
        public Guid ServiceKey { get; set; }
    }
}