using System;

namespace Chucksoft.Entities.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>The access.</value>
        byte Access { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        string Website { get; set; }

        /// <summary>
        /// Gets or sets the service key.
        /// </summary>
        /// <value>The service key.</value>
        Guid ServiceKey { get; set; }
    }
}