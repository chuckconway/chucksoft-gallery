using System;
using Chucksoft.Entities.Interfaces;

namespace Chucksoft.Entities
{
    public class Photo
    {
        /// <summary>
        /// Gets or sets the date taken.
        /// </summary>
        /// <value>The date taken.</value>
        public DateTime? DateTaken { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>The profile.</value>
        public PhotoProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the photo ID.
        /// </summary>
        /// <value>The photo ID.</value>
        public int PhotoID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the gallery ID.
        /// </summary>
        /// <value>The gallery ID.</value>
        public int GalleryId { get; set; }
    }

    public enum PhotoProfile
    {
        Landscape,
        Portrait
    }
}