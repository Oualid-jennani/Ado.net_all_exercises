create database [prdComdb]
USE [prdComdb]

CREATE TABLE [dbo].[client](
	[numC] [smallint] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](30) NULL,
	[prenom] [varchar](30) NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[numC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[commande]    Script Date: 17/11/2018 12:09:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[commande](
	[numCom] [smallint] IDENTITY(1,1) NOT NULL,
	[dateCom] [date] NULL,
	[numC] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[numCom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ligneCommande]    Script Date: 17/11/2018 12:09:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ligneCommande](
	[numCom] [smallint] NOT NULL,
	[numP] [smallint] NOT NULL,
	[qt] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[numCom] ASC,
	[numP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[produit]    Script Date: 17/11/2018 12:09:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produit](
	[numP] [smallint] IDENTITY(1,1) NOT NULL,
	[libelle] [varchar](30) NULL,
	[pu] [smallmoney] NULL,
PRIMARY KEY CLUSTERED 
(
	[numP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/11/2018 12:09:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[login] [nchar](255) NOT NULL,
	[password] [nchar](255) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[commande]  WITH CHECK ADD FOREIGN KEY([numC])
REFERENCES [dbo].[client] ([numC])
GO
ALTER TABLE [dbo].[ligneCommande]  WITH CHECK ADD FOREIGN KEY([numCom])
REFERENCES [dbo].[commande] ([numCom])
GO
ALTER TABLE [dbo].[ligneCommande]  WITH CHECK ADD FOREIGN KEY([numP])
REFERENCES [dbo].[produit] ([numP])
GO
USE [master]
GO
ALTER DATABASE [prdComdb] SET  READ_WRITE 
GO
