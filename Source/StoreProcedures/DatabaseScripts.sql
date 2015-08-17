/****** Object:  StoredProcedure [dbo].[Album_Update]    Script Date: 06/28/2008 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Update]
( 
	@AlbumID int,
	@Name nvarchar(50),
	@Description nvarchar(150),
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Album 
	SET Name = @Name,
	Description = @Description,
	UserId = @UserId

 
	Where AlbumID = @AlbumID 

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Insert]    Script Date: 06/28/2008 15:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Gallery_Insert]
( 
	@Name nvarchar(50),
	@Description nvarchar(150),
	@AlbumID int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Gallery] (Name, Description, AlbumID, UserId) 
	VALUES (@Name, @Description, @AlbumID, @UserId)

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Delete]    Script Date: 06/28/2008 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Gallery_Delete]
( 
	@GalleryID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Gallery 
	Where GalleryID = @GalleryID 

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_SelectByPrimaryKey]    Script Date: 06/28/2008 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Gallery_SelectByPrimaryKey]
( 
	@GalleryID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select GalleryID, Name, Description, AlbumID, UserId
	From Gallery 
	Where GalleryID = @GalleryID 

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Update]    Script Date: 06/28/2008 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Gallery_Update]
( 
	@GalleryID int,
	@Name nvarchar(50),
	@Description nvarchar(150),
	@AlbumID int,
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Gallery 
	SET 	Name = @Name,
	Description = @Description,
	AlbumID = @AlbumID,
	UserId = @UserId
 
	Where GalleryID = @GalleryID 

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_SelectAll]    Script Date: 06/28/2008 15:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Gallery_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select GalleryID, Name, Description, AlbumID, UserId
	From Gallery

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_Delete]    Script Date: 06/28/2008 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Photo_Delete]
( 
	@PhotoID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Photo 
	Where PhotoID = @PhotoID 

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_SelectByPrimaryKey]    Script Date: 06/28/2008 15:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Photo_SelectByPrimaryKey]
( 
	@PhotoID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select PhotoID, Title, Description, DateTaken, GalleryID, AlbumId 
	From Photo 
	Where PhotoID = @PhotoID 

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_SelectAll]    Script Date: 06/28/2008 15:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Photo_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select PhotoID, Title, Description, Filename, DateTaken, GalleryID 
	From Photo

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_Insert]    Script Date: 06/28/2008 15:24:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Photo_Insert]
( 
	@Title nvarchar(100),
	@Description nvarchar(255),
	@OriginalImage varbinary(max),
	@DisplayImage varbinary(max),
	@ThumbnailImage varbinary(max),
	@AdminThumbnail varbinary(max),
	@AdminFullsizeImage varbinary(max),
	@DateTaken datetime = NULL,
	@GalleryID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Photo] (Title, Description, DateTaken, GalleryID, OriginalImage, DisplayImage, ThumbnailImage, AdminThumbnail, AdminImage) 
	VALUES (@Title, @Description, @DateTaken, @GalleryID, @OriginalImage, @DisplayImage, @ThumbnailImage, @AdminThumbnail, @AdminFullsizeImage)

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_Update]    Script Date: 06/28/2008 15:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Photo_Update]
( 
	@PhotoID int,
	@Title nvarchar(100),
	@Description nvarchar(255),
	@DateTaken datetime,
	@GalleryID int

) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Photo 
	SET 	Title = @Title,
	Description = @Description,
	DateTaken = @DateTaken,
	GalleryID = @GalleryID

 
	Where PhotoID = @PhotoID 

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveThumbnailByPhotoID]    Script Date: 06/28/2008 15:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveThumbnailByPhotoID]
(
	@PhotoID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ThumbnailImage From Photo
	Where PhotoId = @PhotoID
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveOriginalByPhotoID]    Script Date: 06/28/2008 15:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveOriginalByPhotoID]
(
	@PhotoID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT OriginalImage From Photo
	Where PhotoId = @PhotoID
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveDisplayByPhotoID]    Script Date: 06/28/2008 15:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveDisplayByPhotoID]
(
	@PhotoID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DisplayImage From Photo
	Where PhotoId = @PhotoID
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveLatestPhoto]    Script Date: 06/28/2008 15:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveLatestPhoto]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Top 1 Title, Description, DateTaken, PhotoId, GalleryId
	From Photo
	Order by DateTaken desc
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/28/2008 15:24:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Access] [tinyint] NOT NULL CONSTRAINT [DF_User_Access]  DEFAULT ((0)),
	[Website] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_User_CreateDate]  DEFAULT (getutcdate()),
	[ServiceKey] [uniqueidentifier] NOT NULL CONSTRAINT [DF_User_ServiceKey]  DEFAULT (newid()),
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_User_Deleted]  DEFAULT ((0)),
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Album_Insert]    Script Date: 06/28/2008 15:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Insert]
( 
	@Name nvarchar(50),
	@Description nvarchar(150),
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Album] (Name, Description, UserId) 
	VALUES (@Name, @Description, @UserId)

END
GO
/****** Object:  StoredProcedure [dbo].[Album_Delete]    Script Date: 06/28/2008 15:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Album_Delete]
( 
	@AlbumID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From Album 
	Where AlbumID = @AlbumID 

END
GO
/****** Object:  StoredProcedure [dbo].[Album_SelectByPrimaryKey]    Script Date: 06/28/2008 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Album_SelectByPrimaryKey]
( 
	@AlbumID int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select AlbumID, Name, Description, UserId
	From Album 
	Where AlbumID = @AlbumID 

END
GO
/****** Object:  StoredProcedure [dbo].[Album_SelectAll]    Script Date: 06/28/2008 15:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[Album_SelectAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select AlbumID, Name, Description, UserId
	From Album
	Order by Name asc

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_SelectAllPhotosByAlbumId]    Script Date: 06/28/2008 15:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_SelectAllPhotosByAlbumId]
(
	@AlbumId int
)
AS
BEGIN

	SELECT     dbo.Photo.PhotoID, dbo.Album.AlbumID, dbo.Album.Name, dbo.Album.Description, dbo.Album.UserId, dbo.Photo.Title, dbo.Photo.Description AS Expr1, 
						  dbo.Photo.DateTaken, dbo.Photo.AlbumId AS Expr2, dbo.Photo.GalleryID
	FROM         dbo.Album INNER JOIN
						  dbo.Photo ON dbo.Album.AlbumID = dbo.Photo.AlbumId
	WHERE     dbo.Photo.GalleryId = 0 and dbo.Photo.AlbumId = @AlbumId

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_SelectAllGalleriesByUserId]    Script Date: 06/28/2008 15:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gallery_SelectAllGalleriesByUserId]
-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN

	Select GalleryID, [Name], [Description], AlbumId, UserId 
	FROM Gallery
	Where UserId = @UserId
	Order BY [Name] asc

END
GO
/****** Object:  StoredProcedure [dbo].[User_EmailAddressCount]    Script Date: 06/28/2008 15:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_EmailAddressCount]
(
	@EmailAddress nvarchar(150)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select count(*) 
	From [User]
	Where Email = @EmailAddress

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_SelectRandomImageByUserID]    Script Date: 06/28/2008 15:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_SelectRandomImageByUserID]
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP (1) dbo.Photo.PhotoID
	FROM dbo.Album INNER JOIN
         dbo.Gallery ON dbo.Album.AlbumID = dbo.Gallery.AlbumID INNER JOIN
         dbo.Photo ON dbo.Gallery.GalleryID = dbo.Photo.GalleryID
	Where (Photo.AlbumId <> 0 AND Photo.GalleryID <> 0) 
	And (Album.UserId = @UserId  OR Gallery.UserId = @UserId )
	Order By NewID()
END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_SelectAllGalleriesByUserIdWithPhotoCount]    Script Date: 06/28/2008 15:24:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Gallery_SelectAllGalleriesByUserIdWithPhotoCount]
-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN

	With UserImages(GalleryID, UserID, PhotoCount)
	AS
	(
		Select Gallery.GalleryID, UserID, Count(PhotoID) as PhotoCount
		From Photo Inner Join Gallery On Photo.GalleryID = Gallery.GalleryID
		Where UserID = @UserId
		Group BY Gallery.GalleryID, UserID

	)

	Select GalleryID, [Name], [Description], AlbumId, UserId, 
	(
	    SELECT PhotoCount From UserImages Where UserImages.GalleryID = Gallery.GalleryID
	) 
	AS PhotoCount
	FROM Gallery
	Where UserId = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveAllPhotosByUserId]    Script Date: 06/28/2008 15:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveAllPhotosByUserId] 
	-- Add the parameters for the stored procedure here
(
	@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     dbo.Photo.Title, dbo.Photo.PhotoID, dbo.Photo.Description, dbo.Photo.DateTaken, dbo.Photo.GalleryID, dbo.Gallery.UserId
	FROM       dbo.Gallery INNER JOIN
			   dbo.Photo ON dbo.Gallery.GalleryID = dbo.Photo.GalleryID
	Where UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveAllPhotosByUserIdAndGalleryId]    Script Date: 06/28/2008 15:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrieveAllPhotosByUserIdAndGalleryId] 
	-- Add the parameters for the stored procedure here
(
	@UserId int,
	@GalleryId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     dbo.Photo.Title, dbo.Photo.PhotoID, dbo.Photo.Description, dbo.Photo.DateTaken, dbo.Photo.GalleryID, dbo.Gallery.UserId
	FROM       dbo.Gallery INNER JOIN
			   dbo.Photo ON dbo.Gallery.GalleryID = dbo.Photo.GalleryID
	Where UserId = @UserId AND Gallery.GalleryId = @GalleryId
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrievePhotosByGalleryId]    Script Date: 06/28/2008 15:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetrievePhotosByGalleryId] 
	-- Add the parameters for the stored procedure here
(
	@GalleryId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Title, Description, DateTaken, PhotoId, GalleryId
	From Photo 
	Where GalleryId = @GalleryId

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetreiveRandomPhoto]    Script Date: 06/28/2008 15:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_RetreiveRandomPhoto]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select newid() as randomId, photoId, Title, Description, DateTaken, GalleryID
	from Photo
	order by randomId

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetreiveRandomPhotoByGalleryId]    Script Date: 06/28/2008 15:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[Photo_RetreiveRandomPhotoByGalleryId]
(
	@GalleryId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select newid() as randomId, photoId, Title, Description, DateTaken, GalleryID
	from Photo
	Where GalleryId = @GalleryId
	order by randomId


END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveAdminImageByPhotoID]    Script Date: 06/28/2008 15:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Photo_RetrieveAdminImageByPhotoID]
(
	@PhotoID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AdminImage From Photo
	Where PhotoId = @PhotoID
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_RetrieveAdminThumbnailByPhotoID]    Script Date: 06/28/2008 15:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Photo_RetrieveAdminThumbnailByPhotoID]
(
	@PhotoID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AdminThumbnail From Photo
	Where PhotoId = @PhotoID
END
GO
/****** Object:  StoredProcedure [dbo].[Photo_UpdateMetaData]    Script Date: 06/28/2008 15:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Wednesday, April 09, 2008
-- =============================================
Create PROCEDURE  [dbo].[Photo_UpdateMetaData]
( 
	@PhotoID int,
	@Title nvarchar(100),
	@Description nvarchar(255),
	@DateTaken datetime
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update Photo 
	SET 	Title = @Title,
	Description = @Description,
	DateTaken = @DateTaken

 
	Where PhotoID = @PhotoID 

END
GO
/****** Object:  StoredProcedure [dbo].[Photo_MovePhotoToNewGallery]    Script Date: 06/28/2008 15:24:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Photo_MovePhotoToNewGallery] 
(
	@PhotoId int,
	@GalleryId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	Update Photo 
	Set GalleryID = @GalleryId
	Where PhotoID = @PhotoId;
END
GO
/****** Object:  Table [dbo].[Album]    Script Date: 06/28/2008 15:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[AlbumID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Album_CreateDate]  DEFAULT (getutcdate()),
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 06/28/2008 15:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[GalleryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[AlbumID] [int] NULL,
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Gallery_CreateDate]  DEFAULT (getutcdate()),
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GalleryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 06/28/2008 15:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Photo](
	[PhotoID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[DateTaken] [datetime] NULL,
	[GalleryID] [int] NOT NULL,
	[OriginalImage] [varbinary](max) NOT NULL,
	[DisplayImage] [varbinary](max) NOT NULL,
	[ThumbnailImage] [varbinary](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Photo_CreateDate]  DEFAULT (getutcdate()),
	[AdminImage] [varbinary](max) NOT NULL,
	[AdminThumbnail] [varbinary](max) NOT NULL,
	[ImageType] [nvarchar](50) NOT NULL CONSTRAINT [DF_Photo_ImageType]  DEFAULT (N'jpg'),
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[PhotoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[User_RetrieveUserByUsernameAndPassword]    Script Date: 06/28/2008 15:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_RetrieveUserByUsernameAndPassword]
	-- Add the parameters for the stored procedure here
(
	@Username nvarchar(150),
	@Password nvarchar(150)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey
	From [User]
	Where Email = @Username AND Password = @Password And Deleted <> 1
    -- Insert statements for procedure here
	
END
GO
/****** Object:  StoredProcedure [dbo].[User_RetrieveUserCountByUsernameAndPassword]    Script Date: 06/28/2008 15:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_RetrieveUserCountByUsernameAndPassword]
	-- Add the parameters for the stored procedure here
(
	@Username nvarchar (150),
	@Password nvarchar (150)
)
AS
BEGIN

	Select count(*) From [User]
	Where Email = @Username AND Password = @Password AND Deleted <> 1

END
GO
/****** Object:  StoredProcedure [dbo].[User_RetrieveUserByUserID]    Script Date: 06/28/2008 15:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_RetrieveUserByUserID]
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select UserId, Email, Password, FirstName, LastName, Access, ServiceKey, Website
	From [User] 
	Where UserId = @UserId 

END
GO
/****** Object:  StoredProcedure [dbo].[User_RetrieveAll]    Script Date: 06/28/2008 15:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_RetrieveAll]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey
	From [User]
	Where Deleted <> 1

END
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 06/28/2008 15:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
Create PROCEDURE  [dbo].[User_Delete]
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [User] 
	SET 	Deleted = 1
 
	Where UserId = @UserId 

END
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 06/28/2008 15:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_Insert]
( 
	@Email nvarchar(50),
	@Password nvarchar(150),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Access tinyint,
	@Website nvarchar(200)
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User] (Email, Password, FirstName, LastName, Access, Website) 
	VALUES (@Email, @Password, @FirstName, @LastName, @Access, @Website)

END
GO
/****** Object:  StoredProcedure [dbo].[User_SelectByPrimaryKey]    Script Date: 06/28/2008 15:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_SelectByPrimaryKey]
( 
	@UserId int
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select UserId, Email, Password, FirstName, LastName, Access, ServiceKey
	From [User] 
	Where UserId = @UserId 

END
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 06/28/2008 15:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_Update]
( 
	@UserId int,
	@Email nvarchar(50),
	@Password nvarchar(150),
	@FirstName nvarchar(50), 
	@LastName nvarchar(50),
	@Access tinyint,
	@Website nvarchar(200)
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Update [User] 
	SET 	Email = @Email,
	Password = @Password,
	FirstName = @FirstName,
	LastName = @LastName,
	Access = @Access,
	Website = @Website
 
	Where UserId = @UserId 

END
GO
/****** Object:  StoredProcedure [dbo].[User_SelectByServiceKey]    Script Date: 06/28/2008 15:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Chucksoft CodeGen
-- Create date: Tuesday, April 15, 2008
-- =============================================
CREATE PROCEDURE  [dbo].[User_SelectByServiceKey]
( 
	@ServiceKey uniqueidentifier
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select UserId, Email, Password, FirstName, LastName, Access, Website, ServiceKey
	From [User] 
	Where ServiceKey = @ServiceKey 

END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_DeleteGalleryAndMovePhotos]    Script Date: 06/28/2008 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gallery_DeleteGalleryAndMovePhotos]
(
	@GalleryId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
	
	Begin Transaction

	Declare @NewGalleryId int
	Set @NewGalleryId = (Select top(1) GalleryId From Gallery Where GalleryID <> @GalleryId Order by CreateDate Desc)

	Update Photo Set GalleryID = @NewGalleryId
	Where  GalleryID = @GalleryId

	Delete From Gallery Where GalleryID = @GalleryId

	Commit 
	
	End TRY
	Begin Catch
		Rollback
	End Catch	
	
END
GO
/****** Object:  StoredProcedure [dbo].[Gallery_SelectAllGalleriesWithPhotoCount]    Script Date: 06/28/2008 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Gallery_SelectAllGalleriesWithPhotoCount]
AS
BEGIN

	With UserImages(GalleryID, PhotoCount)
	AS
	(
		Select Gallery.GalleryID, Count(PhotoID) as PhotoCount
		From Photo Inner Join Gallery On Photo.GalleryID = Gallery.GalleryID
		Group BY Gallery.GalleryID

	)

	Select GalleryID, [Name], [Description], AlbumId, 
	(
	    SELECT PhotoCount From UserImages Where UserImages.GalleryID = Gallery.GalleryID
	) 
	AS PhotoCount
	FROM Gallery
	Order By CreateDate Desc


END
GO
/****** Object:  ForeignKey [FK_Album_User1]    Script Date: 06/28/2008 15:24:14 ******/
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_User1] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_User1]
GO
/****** Object:  ForeignKey [FK_Gallery_Album]    Script Date: 06/28/2008 15:24:17 ******/
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_Album] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Album] ([AlbumID])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_Album]
GO
/****** Object:  ForeignKey [FK_Gallery_User]    Script Date: 06/28/2008 15:24:17 ******/
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_User]
GO
/****** Object:  ForeignKey [FK_Photo_Gallery]    Script Date: 06/28/2008 15:24:21 ******/
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Gallery] FOREIGN KEY([GalleryID])
REFERENCES [dbo].[Gallery] ([GalleryID])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Gallery]
GO
