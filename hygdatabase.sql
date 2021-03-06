USE [hyg]
GO
/****** Object:  Table [dbo].[Zvijezda]    Script Date: 19/01/2020 16:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zvijezda](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[proper] [varchar](50) NULL,
	[dist] [varchar](50) NULL,
	[mag] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
