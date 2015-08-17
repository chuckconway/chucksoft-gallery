using System;

namespace Chucksoft.Entities.Interfaces
{
    public interface IPhoto
    {
        /// <summary>
        /// Gets or sets the date taken.
        /// </summary>
        /// <value>The date taken.</value>
        DateTime? DateTaken { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>The profile.</value>
        PhotoProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the photo ID.
        /// </summary>
        /// <value>The photo ID.</value>
        int PhotoID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the gallery ID.
        /// </summary>
        /// <value>The gallery ID.</value>
        int GalleryId { get; set; }
    }
}