USE [master]
GO
/****** Object:  Database [InteriorConstructionQuotationSystem]    Script Date: 27/02/2024 8:19:22 CH ******/
CREATE DATABASE [InteriorConstructionQuotationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InteriorConstructionQuotationSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\InteriorConstructionQuotationSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'InteriorConstructionQuotationSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\InteriorConstructionQuotationSystem_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InteriorConstructionQuotationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
USE [InteriorConstructionQuotationSystem]
GO
/****** Object:  Table [dbo].[article]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[article](
	[article_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[content] [nvarchar](255) NULL,
	[article_type_id] [int] NOT NULL,
	[created_at] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[article_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[article_types]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[article_types](
	[article_type_id] [int] IDENTITY(1,1) NOT NULL,
	[article_type_name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[article_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[completedProject]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[completedProject](
	[project_id] [int] IDENTITY(1,1) NOT NULL,
	[style_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[project_title] [nvarchar](255) NOT NULL,
	[project_description] [nvarchar](255) NULL,
	[project_image] [nvarchar](255) NULL,
	[startDate] [date] NULL,
	[endDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[project_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contract]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contract](
	[contract_id] [int] IDENTITY(1,1) NOT NULL,
	[quotation_id] [int] NULL,
	[contract_status] [nvarchar](50) NOT NULL,
	[created_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[contract_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[description] [nvarchar](255) NULL,
	[size] [nvarchar](255) NULL,
	[image_url] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductInProject]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInProject](
	[product_id] [int] NOT NULL,
	[project_id] [int] NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_ProductInProject] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[project_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quotation]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quotation](
	[quotation_id] [int] IDENTITY(1,1) NOT NULL,
	[quotation_status] [nvarchar](50) NOT NULL,
	[created_at] [datetime] NOT NULL,
	[style_id] [int] NULL,
	[square] [float] NULL,
	[totalBill] [float] NULL,
	[status] [int] NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[quotation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quotation_detail]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quotation_detail](
	[quotation_d_id] [int] IDENTITY(1,1) NOT NULL,
	[quotation_id] [int] NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__quotatio__F90A1A865964317E] PRIMARY KEY CLUSTERED 
(
	[quotation_d_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quotation_temp]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quotation_temp](
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_quotation_temp] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[style]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[style](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[price] [float] NULL,
	[description] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 27/02/2024 8:19:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[fullname] [nvarchar](50) NOT NULL,
	[birthdate] [date] NULL,
	[email] [nvarchar](50) NULL,
	[phone_number] [nvarchar](15) NULL,
	[avt_url] [nvarchar](255) NULL,
	[role_id] [int] NOT NULL,
	[token] [nvarchar](3000) NULL,
	[expireDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[article] ON 

INSERT [dbo].[article] ([article_id], [user_id], [title], [content], [article_type_id], [created_at]) VALUES (2, 3, N'How to Choose the Perfect Color Palette', N'Choosing the right colors for your home can be daunting, but with these tips, you can create a harmonious color scheme that reflects your style.', 1, CAST(N'2024-02-23' AS Date))
INSERT [dbo].[article] ([article_id], [user_id], [title], [content], [article_type_id], [created_at]) VALUES (3, 3, N'10 Creative Ideas for Small Spaces', N'Make the most out of your small space with these innovative design ideas. From multi-functional furniture to clever storage solutions, discover ways to maximize your living area.', 1, CAST(N'2024-02-23' AS Date))
INSERT [dbo].[article] ([article_id], [user_id], [title], [content], [article_type_id], [created_at]) VALUES (4, 2, N'The Impact of Lighting on Interior Design', N'Lighting plays a crucial role in interior design, affecting the ambiance and functionality of a space. Learn how to use different lighting techniques to enhance your home.', 2, CAST(N'2024-02-24' AS Date))
INSERT [dbo].[article] ([article_id], [user_id], [title], [content], [article_type_id], [created_at]) VALUES (5, 2, N'Tips for Creating a Cozy Bedroom Retreat', N'Transform your bedroom into a cozy sanctuary with these simple tips. From soft bedding to ambient lighting, create a relaxing atmosphere for a good night''s sleep.', 2, CAST(N'2024-02-25' AS Date))
SET IDENTITY_INSERT [dbo].[article] OFF
GO
SET IDENTITY_INSERT [dbo].[article_types] ON 

INSERT [dbo].[article_types] ([article_type_id], [article_type_name]) VALUES (1, N'News')
INSERT [dbo].[article_types] ([article_type_id], [article_type_name]) VALUES (2, N'Blog')
INSERT [dbo].[article_types] ([article_type_id], [article_type_name]) VALUES (3, N'Exp')
INSERT [dbo].[article_types] ([article_type_id], [article_type_name]) VALUES (4, N'Sale blog')
SET IDENTITY_INSERT [dbo].[article_types] OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (1, N'Desk')
INSERT [dbo].[categories] ([id], [name]) VALUES (2, N'Chair')
INSERT [dbo].[categories] ([id], [name]) VALUES (3, N'Sofa')
INSERT [dbo].[categories] ([id], [name]) VALUES (4, N'Cabinet')
INSERT [dbo].[categories] ([id], [name]) VALUES (5, N'Bed')
INSERT [dbo].[categories] ([id], [name]) VALUES (6, N'Bookcase')
INSERT [dbo].[categories] ([id], [name]) VALUES (7, N'Dining Table')
INSERT [dbo].[categories] ([id], [name]) VALUES (8, N'Work Table')
INSERT [dbo].[categories] ([id], [name]) VALUES (9, N'Coffee Table')
INSERT [dbo].[categories] ([id], [name]) VALUES (10, N'Pillow')
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[completedProject] ON 

INSERT [dbo].[completedProject] ([project_id], [style_id], [user_id], [project_title], [project_description], [project_image], [startDate], [endDate]) VALUES (1, 1, 2, N'aaaa', N'123', N'122', CAST(N'2023-04-04' AS Date), CAST(N'2024-05-05' AS Date))
SET IDENTITY_INSERT [dbo].[completedProject] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (11, 1, 4, N'Wooden Desk', CAST(150.00 AS Decimal(10, 2)), N'A sturdy wooden desk suitable for study or work.', N'120x60x75 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (12, 1, 4, N'Glass Coffee Table', CAST(120.00 AS Decimal(10, 2)), N'Modern glass coffee table with sleek metal legs.', N'90x60x45 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (13, 1, 4, N'Round Dining Table', CAST(300.00 AS Decimal(10, 2)), N'Elegant round dining table with a marble top.', N'120x120x75 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (14, 1, 4, N'Foldable Card Table', CAST(50.00 AS Decimal(10, 2)), N'Portable foldable table ideal for card games or outdoor events.', N'80x80x70 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (15, 1, 4, N'Adjustable Standing Desk', CAST(250.00 AS Decimal(10, 2)), N'Height-adjustable desk to support sitting and standing work.', N'120x70x75 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (16, 2, 4, N'Leather Office Chair', CAST(120.00 AS Decimal(10, 2)), N'Comfortable leather office chair with ergonomic design.', N'60x60x100 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (17, 2, 4, N'Modern Armchair', CAST(180.00 AS Decimal(10, 2)), N'Sleek and stylish armchair for lounging.', N'70x80x90 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (18, 2, 4, N'Wooden Dining Chair', CAST(80.00 AS Decimal(10, 2)), N'Classic wooden dining chair with upholstered seat.', N'50x50x90 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (19, 2, 4, N'Folding Lawn Chair', CAST(40.00 AS Decimal(10, 2)), N'Foldable lawn chair for outdoor relaxation.', N'60x60x90 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (20, 2, 4, N'Rocking Chair', CAST(150.00 AS Decimal(10, 2)), N'Traditional wooden rocking chair for serene moments.', N'65x90x100 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (21, 3, 4, N'L-shaped Sectional Sofa', CAST(800.00 AS Decimal(10, 2)), N'Spacious L-shaped sectional sofa with chaise lounge.', N'250x200x80 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (22, 3, 4, N'Velvet Loveseat', CAST(450.00 AS Decimal(10, 2)), N'Luxurious velvet loveseat perfect for cozy seating.', N'150x80x85 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (23, 3, 4, N'Reclining Sofa', CAST(700.00 AS Decimal(10, 2)), N'Comfortable reclining sofa with plush cushioning.', N'200x90x100 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (24, 3, 4, N'Convertible Futon', CAST(250.00 AS Decimal(10, 2)), N'Versatile convertible futon for seating and sleeping.', N'180x90x80 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (25, 3, 4, N'Mid-Century Modern Sofa', CAST(600.00 AS Decimal(10, 2)), N'Chic mid-century modern sofa with tapered legs.', N'180x80x85 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (26, 4, 5, N'Bookcase with Doors', CAST(200.00 AS Decimal(10, 2)), N'Tall bookcase with glass doors for displaying and storing books.', N'80x30x180 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (27, 4, 5, N'Fabric Storage Bins', CAST(30.00 AS Decimal(10, 2)), N'Set of fabric storage bins for organizing clothes, toys, or accessories.', N'30x30x30 cm (each)', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (28, 4, 5, N'Wooden Shoe Rack', CAST(60.00 AS Decimal(10, 2)), N'Sturdy wooden shoe rack to keep footwear organized.', N'80x25x45 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (29, 4, 5, N'Stackable Storage Cubes', CAST(50.00 AS Decimal(10, 2)), N'Modular stackable storage cubes for customizable organization.', N'40x40x40 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (30, 4, 5, N'Clothes Wardrobe', CAST(300.00 AS Decimal(10, 2)), N'Large clothes wardrobe with hanging rod and shelves.', N'120x50x180 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (31, 5, 5, N'Platform Bed Frame', CAST(350.00 AS Decimal(10, 2)), N'Sturdy platform bed frame with wooden slats for mattress support.', N'160x200 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (32, 5, 5, N'Upholstered Bed with Storage', CAST(700.00 AS Decimal(10, 2)), N'Upholstered bed frame with built-in storage drawers.', N'160x200 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (33, 5, 5, N'Metal Canopy Bed', CAST(450.00 AS Decimal(10, 2)), N'Elegant metal canopy bed frame with headboard and footboard.', N'160x200 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (34, 5, 5, N'Bunk Bed with Trundle', CAST(550.00 AS Decimal(10, 2)), N'Space-saving bunk bed with pull-out trundle for additional sleeping space.', N'90x200 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (35, 5, 5, N'Adjustable Bed Base', CAST(800.00 AS Decimal(10, 2)), N'Adjustable bed base with wireless remote control for personalized comfort.', N'90x200 cm', NULL, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (36, 6, 5, N'Tall Wooden Bookcase', CAST(250.00 AS Decimal(10, 2)), N'Tall wooden bookcase with adjustable shelves for organizing books and display items.', N'80x30x180 cm', NULL, CAST(N'2023-01-15T00:00:00.000' AS DateTime), CAST(N'2023-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (37, 6, 5, N'Corner Bookshelf', CAST(120.00 AS Decimal(10, 2)), N'Space-saving corner bookshelf with multiple tiers for maximizing storage.', N'60x60x180 cm', NULL, CAST(N'2023-01-16T00:00:00.000' AS DateTime), CAST(N'2023-01-16T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (38, 6, 5, N'Industrial Pipe Bookcase', CAST(180.00 AS Decimal(10, 2)), N'Industrial-style bookcase featuring metal pipes and wooden shelves.', N'100x40x180 cm', NULL, CAST(N'2023-01-17T00:00:00.000' AS DateTime), CAST(N'2023-01-17T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (39, 6, 5, N'Rotating Bookcase', CAST(300.00 AS Decimal(10, 2)), N'Innovative rotating bookcase with multiple compartments for efficient storage.', N'90x90x180 cm', NULL, CAST(N'2023-01-18T00:00:00.000' AS DateTime), CAST(N'2023-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (40, 6, 5, N'Modular Cube Storage', CAST(150.00 AS Decimal(10, 2)), N'Modular cube storage system that can be customized to fit different spaces.', N'120x30x120 cm', NULL, CAST(N'2023-01-19T00:00:00.000' AS DateTime), CAST(N'2023-01-19T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (41, 7, 4, N'Rectangular Wooden Dining Table', CAST(350.00 AS Decimal(10, 2)), N'Classic rectangular wooden dining table with sturdy legs.', N'150x80x75 cm', NULL, CAST(N'2023-01-20T00:00:00.000' AS DateTime), CAST(N'2023-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (42, 7, 4, N'Extendable Dining Table', CAST(500.00 AS Decimal(10, 2)), N'Versatile extendable dining table with hidden leaf for accommodating extra guests.', N'120x80x75 cm', NULL, CAST(N'2023-01-21T00:00:00.000' AS DateTime), CAST(N'2023-01-21T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (43, 7, 4, N'Round Marble Dining Table', CAST(700.00 AS Decimal(10, 2)), N'Elegant round dining table with luxurious marble top.', N'120x120x75 cm', NULL, CAST(N'2023-01-22T00:00:00.000' AS DateTime), CAST(N'2023-01-22T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (44, 7, 4, N'Glass Top Dining Table', CAST(450.00 AS Decimal(10, 2)), N'Modern dining table featuring a sleek glass top and metal base.', N'100x100x75 cm', NULL, CAST(N'2023-01-23T00:00:00.000' AS DateTime), CAST(N'2023-01-23T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (45, 7, 4, N'Farmhouse Dining Table', CAST(400.00 AS Decimal(10, 2)), N'Rustic farmhouse dining table with distressed finish and turned legs.', N'180x90x75 cm', NULL, CAST(N'2023-01-24T00:00:00.000' AS DateTime), CAST(N'2023-01-24T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (46, 8, 4, N'Industrial Writing Desk', CAST(200.00 AS Decimal(10, 2)), N'Sturdy industrial-style writing desk with metal frame and wooden top.', N'120x60x75 cm', NULL, CAST(N'2023-01-25T00:00:00.000' AS DateTime), CAST(N'2023-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (47, 8, 4, N'Corner Computer Desk', CAST(150.00 AS Decimal(10, 2)), N'Space-saving corner computer desk with built-in shelves.', N'100x100x75 cm', NULL, CAST(N'2023-01-26T00:00:00.000' AS DateTime), CAST(N'2023-01-26T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (48, 8, 4, N'Wall-Mounted Folding Desk', CAST(120.00 AS Decimal(10, 2)), N'Compact wall-mounted folding desk for small spaces.', N'80x40x60 cm (when folded)', NULL, CAST(N'2023-01-27T00:00:00.000' AS DateTime), CAST(N'2023-01-27T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (49, 8, 4, N'Adjustable Drafting Table', CAST(180.00 AS Decimal(10, 2)), N'Height-adjustable drafting table with tilting surface for artists and designers.', N'120x70x75 cm', NULL, CAST(N'2023-01-28T00:00:00.000' AS DateTime), CAST(N'2023-01-28T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (50, 8, 4, N'Minimalist Study Desk', CAST(100.00 AS Decimal(10, 2)), N'Sleek and minimalist study desk with slim profile.', N'100x50x75 cm', NULL, CAST(N'2023-01-29T00:00:00.000' AS DateTime), CAST(N'2023-01-29T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (51, 9, 4, N'Oval Glass Coffee Table', CAST(180.00 AS Decimal(10, 2)), N'Modern oval-shaped coffee table with tempered glass top.', N'120x60x45 cm', NULL, CAST(N'2023-01-30T00:00:00.000' AS DateTime), CAST(N'2023-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (52, 9, 4, N'Rectangular Wooden Coffee Table', CAST(150.00 AS Decimal(10, 2)), N'Simple rectangular wooden coffee table with lower shelf for storage.', N'100x50x45 cm', NULL, CAST(N'2023-01-31T00:00:00.000' AS DateTime), CAST(N'2023-01-31T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (53, 9, 4, N'Marble Top Coffee Table', CAST(250.00 AS Decimal(10, 2)), N'Elegant coffee table featuring a genuine marble top and metal frame.', N'90x90x40 cm', NULL, CAST(N'2023-02-01T00:00:00.000' AS DateTime), CAST(N'2023-02-01T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (54, 9, 4, N'Round Brass Coffee Table', CAST(220.00 AS Decimal(10, 2)), N'Sophisticated round coffee table with brass finish and glass top.', N'80x80x45 cm', NULL, CAST(N'2023-02-02T00:00:00.000' AS DateTime), CAST(N'2023-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (55, 9, 4, N'Wooden Crate Coffee Table', CAST(120.00 AS Decimal(10, 2)), N'Rustic wooden crate coffee table with distressed finish and storage compartments.', N'80x80x30 cm', NULL, CAST(N'2023-02-03T00:00:00.000' AS DateTime), CAST(N'2023-02-03T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (56, 10, 5, N'Memory Foam Pillow', CAST(30.00 AS Decimal(10, 2)), N'Contoured memory foam pillow for optimal head and neck support.', N'60x40 cm', NULL, CAST(N'2023-02-04T00:00:00.000' AS DateTime), CAST(N'2023-02-04T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (57, 10, 5, N'Down Feather Pillow', CAST(40.00 AS Decimal(10, 2)), N'Luxurious down feather pillow with soft and plush feel.', N'50x70 cm', NULL, CAST(N'2023-02-05T00:00:00.000' AS DateTime), CAST(N'2023-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (58, 10, 5, N'Body Pillow', CAST(50.00 AS Decimal(10, 2)), N'Long body pillow for full-body support and comfort during sleep.', N'150x50 cm', NULL, CAST(N'2023-02-06T00:00:00.000' AS DateTime), CAST(N'2023-02-06T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (59, 10, 5, N'Decorative Throw Pillow', CAST(20.00 AS Decimal(10, 2)), N'Stylish decorative throw pillow to add a pop of color to any space.', N'45x45 cm', NULL, CAST(N'2023-02-07T00:00:00.000' AS DateTime), CAST(N'2023-02-07T00:00:00.000' AS DateTime))
INSERT [dbo].[product] ([product_id], [category_id], [user_id], [name], [price], [description], [size], [image_url], [created_at], [updated_at]) VALUES (60, 10, 5, N'Lumbar Support Pillow', CAST(35.00 AS Decimal(10, 2)), N'Ergonomic lumbar support pillow for relieving back pain and improving posture.', N'30x50 cm', NULL, CAST(N'2023-02-08T00:00:00.000' AS DateTime), CAST(N'2023-02-08T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[product] OFF
GO
INSERT [dbo].[ProductInProject] ([product_id], [project_id], [quantity]) VALUES (11, 1, 2)
INSERT [dbo].[ProductInProject] ([product_id], [project_id], [quantity]) VALUES (12, 1, 3)
INSERT [dbo].[ProductInProject] ([product_id], [project_id], [quantity]) VALUES (13, 1, 1)
GO
INSERT [dbo].[quotation_temp] ([user_id], [product_id], [quantity]) VALUES (2, 11, 4)
INSERT [dbo].[quotation_temp] ([user_id], [product_id], [quantity]) VALUES (2, 12, 2)
INSERT [dbo].[quotation_temp] ([user_id], [product_id], [quantity]) VALUES (2, 13, 3)
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [role_name]) VALUES (1, N'admin')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (2, N'customer')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (3, N'staff')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (4, N'Admin')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (5, N'Staff')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (6, N'Customer')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[style] ON 

INSERT [dbo].[style] ([id], [name], [price], [description]) VALUES (1, N'Italian', 75000, NULL)
INSERT [dbo].[style] ([id], [name], [price], [description]) VALUES (2, N'Japanese', 55000, NULL)
INSERT [dbo].[style] ([id], [name], [price], [description]) VALUES (3, N'American', 85000, NULL)
SET IDENTITY_INSERT [dbo].[style] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (2, N'admin1', N'admin@@', N'John Doe', CAST(N'1990-01-01' AS Date), N'admin1@gmail.com', N'0909121212', NULL, 1, N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4xIiwiVXNlcklEIjoiMiIsIkZ1bGxOYW1lIjoiSm9obiBEb2UiLCJSb2xlIjoiYWRtaW4iLCJleHAiOjE3MDkwMzk2NzV9.OlAO83gepgIJjiSYWu3ZojYwgrt5RcjOqEjVS9Nby9qf522hwHJEWc681QSxbc-KZsPud4D6XnSLLkj8mZPkEg', CAST(N'2024-02-27T20:14:35.593' AS DateTime))
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (3, N'admin2', N'admin@@', N'Jane Smith', CAST(N'1995-05-15' AS Date), N'admin2@gmail.com', N'0707141414', NULL, 1, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (4, N'staff1', N'staff@@', N'Alice Johnson', CAST(N'1988-12-10' AS Date), N'staff1@gmail.com', N'0808161616', NULL, 2, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (5, N'staff2', N'staff@@', N'Johnny Silverhand', CAST(N'1970-11-20' AS Date), N'staff2@gmail.com', N'0290909090', NULL, 2, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (6, N'customer1', N'customer@@', N'Alicia Martinez', CAST(N'1988-12-10' AS Date), N'customer1@gmail.com', N'0768686868', NULL, 3, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (7, N'customer2', N'customer@@', N'Bob Smith', CAST(N'1990-05-20' AS Date), N'customer2@gmail.com', N'0534343434', NULL, 3, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (8, N'customer3', N'customer@@', N'Charlie Brown', CAST(N'1985-08-15' AS Date), N'customer3@gmail.com', N'028798792', NULL, 3, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (9, N'customer4', N'customer@@', N'Diana Taylor', CAST(N'1993-02-28' AS Date), N'customer4@gmail.com', N'0808161616', NULL, 3, NULL, NULL)
INSERT [dbo].[user] ([user_id], [username], [password], [fullname], [birthdate], [email], [phone_number], [avt_url], [role_id], [token], [expireDate]) VALUES (11, N'string', N'string', N'string', CAST(N'2024-02-26' AS Date), N'string', N'string', NULL, 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[article] ADD  DEFAULT (NULL) FOR [content]
GO
ALTER TABLE [dbo].[completedProject] ADD  DEFAULT (NULL) FOR [project_description]
GO
ALTER TABLE [dbo].[completedProject] ADD  DEFAULT (NULL) FOR [project_image]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [description]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [size]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (NULL) FOR [image_url]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[product] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [birthdate]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [phone_number]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [avt_url]
GO
ALTER TABLE [dbo].[article]  WITH CHECK ADD FOREIGN KEY([article_type_id])
REFERENCES [dbo].[article_types] ([article_type_id])
GO
ALTER TABLE [dbo].[article]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[completedProject]  WITH CHECK ADD FOREIGN KEY([style_id])
REFERENCES [dbo].[style] ([id])
GO
ALTER TABLE [dbo].[completedProject]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[contract]  WITH CHECK ADD FOREIGN KEY([quotation_id])
REFERENCES [dbo].[quotation] ([quotation_id])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([id])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[ProductInProject]  WITH CHECK ADD  CONSTRAINT [FK_ProductInProject_completedProject] FOREIGN KEY([project_id])
REFERENCES [dbo].[completedProject] ([project_id])
GO
ALTER TABLE [dbo].[ProductInProject] CHECK CONSTRAINT [FK_ProductInProject_completedProject]
GO
ALTER TABLE [dbo].[ProductInProject]  WITH CHECK ADD  CONSTRAINT [FK_ProductInProject_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[ProductInProject] CHECK CONSTRAINT [FK_ProductInProject_product]
GO
ALTER TABLE [dbo].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_style] FOREIGN KEY([style_id])
REFERENCES [dbo].[style] ([id])
GO
ALTER TABLE [dbo].[quotation] CHECK CONSTRAINT [FK_quotation_style]
GO
ALTER TABLE [dbo].[quotation]  WITH CHECK ADD  CONSTRAINT [FK_quotation_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[quotation] CHECK CONSTRAINT [FK_quotation_user]
GO
ALTER TABLE [dbo].[quotation_detail]  WITH CHECK ADD  CONSTRAINT [FK__quotation__produ__440B1D61] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[quotation_detail] CHECK CONSTRAINT [FK__quotation__produ__440B1D61]
GO
ALTER TABLE [dbo].[quotation_detail]  WITH CHECK ADD  CONSTRAINT [FK_quotation_detail_quotation] FOREIGN KEY([quotation_id])
REFERENCES [dbo].[quotation] ([quotation_id])
GO
ALTER TABLE [dbo].[quotation_detail] CHECK CONSTRAINT [FK_quotation_detail_quotation]
GO
ALTER TABLE [dbo].[quotation_temp]  WITH CHECK ADD  CONSTRAINT [FK_quotation_temp_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[quotation_temp] CHECK CONSTRAINT [FK_quotation_temp_product]
GO
ALTER TABLE [dbo].[quotation_temp]  WITH CHECK ADD  CONSTRAINT [FK_quotation_temp_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[quotation_temp] CHECK CONSTRAINT [FK_quotation_temp_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[contract]  WITH CHECK ADD CHECK  (([contract_status]='inactive' OR [contract_status]='pending' OR [contract_status]='active'))
GO
ALTER TABLE [dbo].[quotation]  WITH CHECK ADD CHECK  (([quotation_status]='inactive' OR [quotation_status]='pending' OR [quotation_status]='active'))
GO
USE [master]
GO
ALTER DATABASE [InteriorConstructionQuotationSystem] SET  READ_WRITE 
GO
