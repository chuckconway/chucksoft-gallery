using System;

namespace Chucksoft.Entities
{
    [Serializable]
    public class Gallery 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gallery"/> class.
        /// </summary>
        public Gallery()
        {
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        public int GalleryId{ get; set; }

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        public int PhotoCount { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the gallery date.
        /// </summary>
        /// <value>The gallery date.</value>
        public DateTime GalleryDate { get; set; }      


        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }
    }
}