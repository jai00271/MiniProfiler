USE [UtilityTool]
GO
/****** Object:  Table [dbo].[TryYourLuck]    Script Date: 1/1/2020 3:34:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TryYourLuck](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numbers] [nvarchar](250) NOT NULL,
	[RemainingCount] [int] NOT NULL,
	[UpdateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
