USE [SerializationObjectDemo]
GO

/****** Object:  Table [dbo].[SerializedData]    Script Date: 06/11/2015 20:35:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SerializedData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataXML] [xml] NULL,
	[DataVarchar] [varchar](max) NULL,
	[DataVarbinary] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


