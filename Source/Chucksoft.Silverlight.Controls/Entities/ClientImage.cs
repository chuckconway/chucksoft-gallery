using System.IO;
using System.Windows.Controls;

namespace Chucksoft.Silverlight.Controls.Entities
{
    public class ClientImage
    {
        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>The name of the image.</value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the gallery.
        /// </summary>
        /// <value>The name of the gallery.</value>
        public string GalleryName { get; set; }

        /// <summary>
        /// Gets or sets the gallery id.
        /// </summary>
        /// <value>The gallery id.</value>
        public int GalleryId { get; set; }

        /// <summary>
        /// Gets or sets the file info.
        /// </summary>
        /// <value>The file info.</value>
        public FileInfo FileInfo { get; set; }

    }
}
