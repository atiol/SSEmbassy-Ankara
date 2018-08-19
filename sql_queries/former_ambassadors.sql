USE [embassy_db]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 08/10/2018 5:25:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FormerAmbassadors](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[ImgUrl] [nvarchar](max) NULL,
	[Position] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[Biography] [nvarchar](max) NULL,
	[JobStartDate] [date] NULL
)
GO
