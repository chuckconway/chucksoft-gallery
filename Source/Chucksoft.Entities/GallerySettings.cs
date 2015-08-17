using System;
using Chucksoft.Entities.Interfaces;
using Conway.IO;
using Conway.Utilities;
using System.IO;

namespace Chucksoft.Entities
{
    [Serializable]
    public class GallerySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GallerySettings"/> class.
        /// </summary>
        public GallerySettings()
        {
            ThumbnailDimensions = new ImageDimension();
            FullsizeDimensions = new ImageDimension();
        }

        /// <summary>
        /// Gets or sets the thumbnail dimensions.
        /// </summary>
        /// <value>The thumbnail dimensions.</value>
        public ImageDimension ThumbnailDimensions { get; set; }

        /// <summary>
        /// Gets or sets the fullsize dimensions.
        /// </summary>
        /// <value>The fullsize dimensions.</value>
        public ImageDimension FullsizeDimensions { get; set; }

        /// <summary>
        /// Title of the Gallery
        /// </summary>
        public string GalleryTitle { get; set; }

        /// <summary>
        /// View of the gallery
        /// </summary>
        public PresentationMode PresentationMode { get; set; }

        /// <summary>
        /// Gets or sets the data storage.
        /// </summary>
        /// <value>The data storage.</value>
        public DataStorage DataStorage { get; set; }

        /// <summary>
        /// Enable albums
        /// </summary>
        public bool EnableAblums { get; set; }

        /// <summary>
        /// Enable themes
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Saves current settings to Disk
        /// </summary>
        public void Save()
        {
            string dbPath = FileUtil.GetExecutingDirectory();
            string settingsFile = dbPath + "\\Settings.config";

            string settings = StringSerialization.SerializeToString(this);
            File.WriteAllText(settingsFile, settings);
        }

        /// <summary>
        /// Loads the settings
        /// </summary>
        /// <returns></returns>
        public static GallerySettings Load()
        {
            string dbPath = FileUtil.GetExecutingDirectory();
            string settingsFile = dbPath + "\\Settings.config";

            GallerySettings settings = new GallerySettings();

            if (File.Exists(settingsFile))
            {
                string settingsText = File.ReadAllText(settingsFile);
                settings = StringSerialization.DeserializeFromString<GallerySettings>(settingsText);
            }

            return settings;
        }
    }

    [Serializable]
    public enum PresentationMode
    {
        Album,
        Gallery,
        Random,
        Single
    }

    [Serializable]
    public enum DataStorage
    {
        SqlCe,
        SqlServer
    }
}
