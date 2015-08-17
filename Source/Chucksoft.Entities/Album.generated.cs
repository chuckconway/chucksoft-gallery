namespace Chucksoft.Entities
{
	public partial class Album : EntityBase 
	{

        /// <summary>
        /// Gets or sets the album ID.
        /// </summary>
        /// <value>The album ID.</value>
		public int AlbumID { get; set; }

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
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

	}
}
