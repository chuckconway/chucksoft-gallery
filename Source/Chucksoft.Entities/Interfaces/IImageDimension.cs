namespace Chucksoft.Entities.Interfaces
{
    public interface IImageDimension
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the photo profile.
        /// </summary>
        /// <value>The photo profile.</value>
        PhotoProfile PhotoProfile { get; set; }
    }
}