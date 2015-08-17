namespace Chucksoft.Entities.Interfaces
{
    public interface IGallery
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        int GalleryId { get; set; }

        /// <summary>
        /// Gets or sets the photo count.
        /// </summary>
        /// <value>The photo count.</value>
        int PhotoCount { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int UserId { get; set; }
    }
}