namespace Chucksoft.Entities.Interfaces
{
    public interface IGalleryPhoto
    {
        /// <summary>
        /// Gets or sets the original image.
        /// </summary>
        /// <value>The original image.</value>
        byte[] OriginalImage { get; set; }

        /// <summary>
        /// Gets or sets the display image.
        /// </summary>
        /// <value>The display image.</value>
        byte[] DisplayImage { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image.
        /// </summary>
        /// <value>The thumbnail image.</value>
        byte[] ThumbnailImage { get; set; }
    }
}
