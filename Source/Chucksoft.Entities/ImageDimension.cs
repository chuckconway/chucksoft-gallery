using System;

namespace Chucksoft.Entities
{
    [Serializable]
    public class ImageDimension 
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the photo profile.
        /// </summary>
        /// <value>The photo profile.</value>
        public PhotoProfile PhotoProfile { get; set; }
    }
}
