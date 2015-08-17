/****** Object:  Table [dbo].[User]    Script Date: 06/03/2009 21:16:55 ******/
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
	[Access] [tinyint] NOT NULL,
	[Website] [nvarchar](200) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ServiceKey] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 06/03/2009 21:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[GalleryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ParentID] [int] NULL,
	[GalleryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GalleryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 06/03/2009 21:16:55 ******/
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
	[CreateDate] [datetime] NOT NULL,
	[AdminImage] [varbinary](max) NOT NULL,
	[AdminThumbnail] [varbinary](max) NOT NULL,
	[ImageType] [nvarchar](50) NOT NULL,
	[Profile] [nvarchar](50) NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[PhotoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Gallery_CreateDate]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Gallery] ADD  CONSTRAINT [DF_Gallery_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Gallery_GalleryDate]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Gallery] ADD  CONSTRAINT [DF_Gallery_GalleryDate]  DEFAULT (getdate()) FOR [GalleryDate]
GO
/****** Object:  Default [DF_Photo_CreateDate]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Photo_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Photo_ImageType]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Photo_ImageType]  DEFAULT (N'jpg') FOR [ImageType]
GO
/****** Object:  Default [DF_Photo_Profile]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Photo] ADD  CONSTRAINT [DF_Photo_Profile]  DEFAULT ('Landscape') FOR [Profile]
GO
/****** Object:  Default [DF_User_Access]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Access]  DEFAULT ((0)) FOR [Access]
GO
/****** Object:  Default [DF_User_CreateDate]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_User_ServiceKey]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ServiceKey]  DEFAULT (newid()) FOR [ServiceKey]
GO
/****** Object:  Default [DF_User_Deleted]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
/****** Object:  ForeignKey [FK_Gallery_User]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_User]
GO
/****** Object:  ForeignKey [FK_Photo_Gallery]    Script Date: 06/03/2009 21:16:55 ******/
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Gallery] FOREIGN KEY([GalleryID])
REFERENCES [dbo].[Gallery] ([GalleryID])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Gallery]
GO
