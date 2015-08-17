using Chucksoft.Entities.Interfaces;

namespace Chucksoft.Entities
{
    public class GalleryPhoto 
    {
        /// <summary>
        /// Gets or sets the original image.
        /// </summary>
        /// <value>The original image.</value>
        public byte[] OriginalImage { get; set; }

        /// <summary>
        /// Gets or sets the display image.
        /// </summary>
        /// <value>The display image.</value>
        public byte[] DisplayImage { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image.
        /// </summary>
        /// <value>The thumbnail image.</value>
        public byte[] ThumbnailImage { get; set; }
    }
}
