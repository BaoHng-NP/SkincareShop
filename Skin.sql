USE [master]
GO
/****** Object:  Database [SkincareShop]    Script Date: 3/25/2025 5:01:56 PM ******/
CREATE DATABASE [SkincareShop]
GO
ALTER DATABASE [SkincareShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SkincareShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SkincareShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SkincareShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SkincareShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SkincareShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SkincareShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [SkincareShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SkincareShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SkincareShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SkincareShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SkincareShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SkincareShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SkincareShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SkincareShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SkincareShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SkincareShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SkincareShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SkincareShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SkincareShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SkincareShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SkincareShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SkincareShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SkincareShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SkincareShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SkincareShop] SET  MULTI_USER 
GO
ALTER DATABASE [SkincareShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SkincareShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SkincareShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SkincareShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SkincareShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SkincareShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SkincareShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [SkincareShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SkincareShop]
GO
/****** Object:  Table [dbo].[brands]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[brands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[brandName] [nvarchar](255) NOT NULL,
	[countryCode] [nvarchar](10) NULL,
	[logo] [nvarchar](255) NULL,
	[isActive] [bit] NOT NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[isActive] [bit] NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[content]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[content](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[contentType] [nvarchar](50) NULL,
	[content] [nvarchar](max) NULL,
	[authorId] [int] NULL,
	[imageUrl] [nvarchar](max) NULL,
	[isPublished] [bit] NULL,
	[publishedAt] [datetime] NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[discounts]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[discountType] [nvarchar](50) NULL,
	[discountValue] [decimal](10, 2) NOT NULL,
	[requiredPoints] [int] NOT NULL,
	[expirationDate] [date] NULL,
	[minOrderValue] [decimal](10, 2) NULL,
	[maxDiscount] [decimal](10, 2) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedbacks]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedbacks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[productId] [int] NULL,
	[rating] [int] NULL,
	[comment] [nvarchar](max) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[loyalty_points]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loyalty_points](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[points] [int] NOT NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_items]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productId] [int] NULL,
	[quantity] [int] NOT NULL,
	[unitPrice] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[address] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](20) NOT NULL,
	[totalPrice] [decimal](10, 2) NOT NULL,
	[paymentMethod] [nvarchar](50) NOT NULL,
	[status] [nvarchar](50) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_details]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productId] [int] NULL,
	[manufactureDate] [date] NULL,
	[expiryDate] [date] NULL,
	[quantity] [int] NULL,
	[createdDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_skin_types]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_skin_types](
	[productId] [int] NOT NULL,
	[skinTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[productId] ASC,
	[skinTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryId] [int] NULL,
	[brandId] [int] NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[stock] [int] NULL,
	[rating] [float] NULL,
	[imageUrl] [nvarchar](max) NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skin_quiz_answers]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skin_quiz_answers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[questionId] [int] NULL,
	[answer] [nvarchar](max) NOT NULL,
	[skinTypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skin_quiz_questions]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skin_quiz_questions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skin_types]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skin_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_skin_tests]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_skin_tests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[skinTypeId] [int] NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_vouchers]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_vouchers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[discountId] [int] NULL,
	[redeemedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/25/2025 5:01:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[phone] [nvarchar](20) NULL,
	[passwordHash] [nvarchar](255) NOT NULL,
	[address] [nvarchar](max) NULL,
	[birthdate] [date] NULL,
	[skinTypeId] [int] NULL,
	[loyaltyPoints] [int] NULL,
	[roleId] [int] NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[brands] ON 

INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (1, N'Klairs', N'KR', N'https://example.com/klairs.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (2, N'COSRX', N'KR', N'https://example.com/cosrx.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (3, N'CeraVe', N'US', N'https://example.com/cerave.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (4, N'La Roche-Posay', N'FR', N'https://example.com/larocheposay.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (5, N'The Ordinary', N'CA', N'https://example.com/theordinary.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (6, N'L''Oréal Paris', N'FR', N'https://example.com/loreal.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (7, N'Neutrogena', N'US', N'https://example.com/neutrogena.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (8, N'Paula''s Choice', N'US', N'https://example.com/paulaschoice.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (9, N'Olay', N'US', N'https://example.com/olay.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (10, N'Cetaphil', N'US', N'https://example.com/cetaphil.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (11, N'Eucerin', N'DE', N'https://example.com/eucerin.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (12, N'Bioderma', N'FR', N'https://example.com/bioderma.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (13, N'Aveeno', N'US', N'https://example.com/aveeno.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (14, N'Kiehl''s', N'US', N'https://example.com/kiehls.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (15, N'SK-II', N'JP', N'https://example.com/sk2.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (16, N'Shiseido', N'JP', N'https://example.com/shiseido.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (17, N'Drunk Elephant', N'US', N'https://example.com/drunkelephant.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (18, N'Tatcha', N'JP', N'https://example.com/tatcha.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
INSERT [dbo].[brands] ([id], [brandName], [countryCode], [logo], [isActive], [createdAt]) VALUES (19, N'Estée Lauder', N'US', N'https://example.com/esteelauder.png', 1, CAST(N'2025-03-16T12:12:06.940' AS DateTime))
SET IDENTITY_INSERT [dbo].[brands] OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (1, N'Cleanser', 1, N'Face wash and cleansing products')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (2, N'Toner', 1, N'Balancing toners for skincare')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (3, N'Serum', 1, N'Skin serums for hydration and treatment')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (4, N'Moisturizer', 1, N'Creams and lotions for hydration')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (5, N'Sunscreen', 1, N'Sun protection products')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (6, N'Essence', 1, N'Lightweight treatment products to hydrate and prep skin')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (7, N'Exfoliator', 1, N'Products that remove dead skin cells')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (8, N'Eye Cream', 1, N'Specialized moisturizers for the delicate eye area')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (9, N'Face Mask', 1, N'Intensive treatment products for various skin concerns')
INSERT [dbo].[categories] ([id], [name], [isActive], [description]) VALUES (10, N'Night Cream', 1, N'Richer formulas designed for overnight use')
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[discounts] ON 

INSERT [dbo].[discounts] ([id], [code], [discountType], [discountValue], [requiredPoints], [expirationDate], [minOrderValue], [maxDiscount], [createdAt]) VALUES (1, N'VOUCHER30K', N'fixed', CAST(30000.00 AS Decimal(10, 2)), 30, CAST(N'2025-12-31' AS Date), CAST(100000.00 AS Decimal(10, 2)), NULL, CAST(N'2025-03-16T12:12:40.887' AS DateTime))
INSERT [dbo].[discounts] ([id], [code], [discountType], [discountValue], [requiredPoints], [expirationDate], [minOrderValue], [maxDiscount], [createdAt]) VALUES (2, N'VOUCHER50K', N'fixed', CAST(50000.00 AS Decimal(10, 2)), 50, CAST(N'2025-12-31' AS Date), CAST(150000.00 AS Decimal(10, 2)), NULL, CAST(N'2025-03-16T12:12:40.887' AS DateTime))
INSERT [dbo].[discounts] ([id], [code], [discountType], [discountValue], [requiredPoints], [expirationDate], [minOrderValue], [maxDiscount], [createdAt]) VALUES (3, N'VOUCHER100K', N'fixed', CAST(100000.00 AS Decimal(10, 2)), 100, CAST(N'2025-12-31' AS Date), CAST(300000.00 AS Decimal(10, 2)), NULL, CAST(N'2025-03-16T12:12:40.887' AS DateTime))
INSERT [dbo].[discounts] ([id], [code], [discountType], [discountValue], [requiredPoints], [expirationDate], [minOrderValue], [maxDiscount], [createdAt]) VALUES (4, N'SUMMER10', N'percentage', CAST(10.00 AS Decimal(10, 2)), 0, CAST(N'2025-12-31' AS Date), CAST(100000.00 AS Decimal(10, 2)), CAST(100000.00 AS Decimal(10, 2)), CAST(N'2025-03-16T12:12:40.887' AS DateTime))
INSERT [dbo].[discounts] ([id], [code], [discountType], [discountValue], [requiredPoints], [expirationDate], [minOrderValue], [maxDiscount], [createdAt]) VALUES (5, N'WELCOME5', N'fixed', CAST(20000.00 AS Decimal(10, 2)), 0, CAST(N'2025-06-30' AS Date), CAST(50000.00 AS Decimal(10, 2)), NULL, CAST(N'2025-03-16T12:12:40.887' AS DateTime))
SET IDENTITY_INSERT [dbo].[discounts] OFF
GO
SET IDENTITY_INSERT [dbo].[feedbacks] ON 

INSERT [dbo].[feedbacks] ([id], [userId], [productId], [rating], [comment], [createdAt]) VALUES (169, 1, 3, 5, N'dấda', CAST(N'2025-03-21T10:56:31.820' AS DateTime))
INSERT [dbo].[feedbacks] ([id], [userId], [productId], [rating], [comment], [createdAt]) VALUES (170, 1, 12, 5, N'đáa', CAST(N'2025-03-21T10:56:31.897' AS DateTime))
SET IDENTITY_INSERT [dbo].[feedbacks] OFF
GO
SET IDENTITY_INSERT [dbo].[order_items] ON 

INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (22, 9, 3, 1, CAST(245000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (23, 9, 6, 1, CAST(450000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (24, 9, 7, 3, CAST(375000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (25, 9, 9, 3, CAST(650000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (27, 14, 1, 12, CAST(350000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (28, 15, 3, 1, CAST(245000.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [quantity], [unitPrice]) VALUES (29, 15, 12, 2, CAST(320000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[order_items] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [userId], [address], [phone], [totalPrice], [paymentMethod], [status], [createdAt]) VALUES (9, 1, N'Hồ Chí Minh', N'012345678', CAST(3770000.00 AS Decimal(10, 2)), N'VNPay', N'Pending', CAST(N'2025-03-19T13:49:03.033' AS DateTime))
INSERT [dbo].[orders] ([id], [userId], [address], [phone], [totalPrice], [paymentMethod], [status], [createdAt]) VALUES (14, 1, N'Hồ Chí Minh', N'012345678', CAST(4200000.00 AS Decimal(10, 2)), N'COD', N'Pending', CAST(N'2025-03-19T16:18:35.973' AS DateTime))
INSERT [dbo].[orders] ([id], [userId], [address], [phone], [totalPrice], [paymentMethod], [status], [createdAt]) VALUES (15, 1, N'Hồ Chí Minh', N'012345678', CAST(885000.00 AS Decimal(10, 2)), N'COD', N'Completed-Feedback', CAST(N'2025-03-21T10:07:23.230' AS DateTime))
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[product_details] ON 

INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (1, 1, CAST(N'2023-01-15' AS Date), CAST(N'2025-01-15' AS Date), 38, CAST(N'2023-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (2, 2, CAST(N'2023-02-10' AS Date), CAST(N'2025-02-10' AS Date), 45, CAST(N'2023-02-10T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (3, 3, CAST(N'2023-01-20' AS Date), CAST(N'2025-01-20' AS Date), 94, CAST(N'2023-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (4, 4, CAST(N'2023-03-05' AS Date), CAST(N'2025-03-05' AS Date), 80, CAST(N'2023-03-05T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (5, 5, CAST(N'2023-02-15' AS Date), CAST(N'2026-02-15' AS Date), 120, CAST(N'2023-02-15T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (6, 6, CAST(N'2023-03-10' AS Date), CAST(N'2025-03-10' AS Date), 60, CAST(N'2023-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (7, 7, CAST(N'2023-04-05' AS Date), CAST(N'2025-04-05' AS Date), 90, CAST(N'2023-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (8, 8, CAST(N'2023-02-25' AS Date), CAST(N'2025-02-25' AS Date), 55, CAST(N'2023-02-25T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (9, 9, CAST(N'2023-01-30' AS Date), CAST(N'2025-01-30' AS Date), 70, CAST(N'2023-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (10, 10, CAST(N'2023-03-15' AS Date), CAST(N'2025-03-15' AS Date), 20, CAST(N'2023-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (11, 11, CAST(N'2023-05-15' AS Date), CAST(N'2025-05-15' AS Date), 40, CAST(N'2023-05-15T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (12, 12, CAST(N'2023-06-10' AS Date), CAST(N'2025-06-10' AS Date), 63, CAST(N'2023-06-10T00:00:00.000' AS DateTime))
INSERT [dbo].[product_details] ([id], [productId], [manufactureDate], [expiryDate], [quantity], [createdDate]) VALUES (13, 13, CAST(N'2023-07-20' AS Date), CAST(N'2025-07-20' AS Date), 30, CAST(N'2023-07-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[product_details] OFF
GO
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (1, 2)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (1, 4)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (1, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (2, 2)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (2, 4)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (2, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (3, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (3, 2)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (4, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (5, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (6, 2)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (6, 4)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (7, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (7, 3)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (8, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (9, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (9, 4)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (9, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (10, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (11, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (11, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (12, 2)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (12, 5)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (13, 1)
INSERT [dbo].[product_skin_types] ([productId], [skinTypeId]) VALUES (13, 4)
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (1, 2, 1, N'Klairs Supple Preparation Toner', N'A gentle, hydrating toner that balances skin pH', CAST(350000.00 AS Decimal(10, 2)), 38, 4.7, N'https://klairsvietnam.vn/wp-content/uploads/2020/07/nuoc-hoa-hong-khong-mui-klairs.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (2, 3, 2, N'COSRX Advanced Snail 96 Mucin Power Essence', N'Hydrating essence with snail secretion filtrate for repair and hydration', CAST(420000.00 AS Decimal(10, 2)), 45, 4.8, N'https://media.loveitopcdn.com/2534/thumb/tinh-chat-oc-sen-giup-duong-am-va-phuc-hoi-lan-da-cosrx-snail-96-mucin-power-essence-100ml-7.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (3, 1, 3, N'CeraVe Hydrating Facial Cleanser', N'Gentle, non-foaming cleanser for normal to dry skin', CAST(245000.00 AS Decimal(10, 2)), 94, 4.5, N'https://bizweb.dktcdn.net/100/407/286/products/cerave-hydrating-facial-cleanser.jpg?v=1621057598607', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (4, 1, 4, N'La Roche-Posay Toleriane Hydrating Gentle Cleanser', N'Gentle cleanser for sensitive skin', CAST(295000.00 AS Decimal(10, 2)), 80, 4.6, N'https://down-vn.img.susercontent.com/file/vn-11134201-7qukw-lj3qquih0l3m8b', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (5, 3, 5, N'The Ordinary Niacinamide 10% + Zinc 1%', N'Serum targeting blemishes and congestion', CAST(199000.00 AS Decimal(10, 2)), 120, 4.3, N'https://ordinary.com.vn/wp-content/uploads/2020/09/The-Ordinary-Niacinamide10-Zinc-1-510x455.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (6, 3, 6, N'L''Oréal Revitalift Hyaluronic Acid Serum', N'Hydrating serum with pure hyaluronic acid', CAST(450000.00 AS Decimal(10, 2)), 60, 4.4, N'https://media.hcdn.vn/catalog/product/g/o/google-shopping-tinh-chat-l-oreal-hyaluronic-acid-cap-am-sang-da-30ml-1.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (7, 4, 7, N'Neutrogena Hydro Boost Water Gel', N'Lightweight gel moisturizer with hyaluronic acid', CAST(375000.00 AS Decimal(10, 2)), 90, 4.7, N'https://product.hstatic.net/1000006063/product/dd_gel_b2879746c8d84a22856364fe9f6cbf1a_1024x1024_d012fe69b2ef4492b80518b6023b210f_1024x1024.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (8, 7, 8, N'Paula''s Choice 2% BHA Liquid Exfoliant', N'Leave-on exfoliant for unclogging pores', CAST(550000.00 AS Decimal(10, 2)), 55, 4.9, N'https://paulaschoice.vn/wp-content/uploads/2019/08/2016-bha-9122020.jpg.webp', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (9, 4, 9, N'Olay Regenerist Micro-Sculpting Cream', N'Anti-aging moisturizer for plumping skin', CAST(650000.00 AS Decimal(10, 2)), 70, 4.5, N'https://www.ubuy.vn/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvNzFNamVTd3ZHK0wuX1NMMTUwMF8uanBn.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (10, 6, 15, N'SK-II Facial Treatment Essence', N'Luxury essence with Pitera for skin renewal', CAST(699000.00 AS Decimal(10, 2)), 20, 4.8, N'https://sieuthilamdep.com/images/detailed/10/nuoc-than-sk-ii-facial-treatment-essence-04.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (11, 5, 4, N'La Roche-Posay Anthelios SPF 50+ Sunscreen', N'High protection sunscreen for sensitive skin', CAST(420000.00 AS Decimal(10, 2)), 40, 4.7, N'https://cosmocosmetics.ae/cdn/shop/files/SUN-SCREEN-LOTION-SPF-50_UVB_UVA-100g.jpg?v=1725449166', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (12, 4, 3, N'CeraVe Moisturizing Cream', N'Rich moisturizer for dry to very dry skin', CAST(320000.00 AS Decimal(10, 2)), 63, 4.8, N'https://edbeauty.vn/wp-content/uploads/2024/10/Moisturizing-Cream-01.png', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
INSERT [dbo].[products] ([id], [categoryId], [brandId], [name], [description], [price], [stock], [rating], [imageUrl], [createdAt]) VALUES (13, 3, 17, N'Radha Beauty Vitamin C Serum', N'Brightening and anti-aging serum with vitamin C', CAST(450000.00 AS Decimal(10, 2)), 30, 4.5, N'https://images-cdn.ubuy.co.in/633ac716946c6908770eec0a-radha-beauty-natural-vitamin-c-serum-for.jpg', CAST(N'2025-03-16T12:12:17.077' AS DateTime))
SET IDENTITY_INSERT [dbo].[products] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([id], [roleName]) VALUES (2, N'Customer')
INSERT [dbo].[roles] ([id], [roleName]) VALUES (1, N'Staff')
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[skin_quiz_answers] ON 

INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (1, 1, N'Normal. No difference compared to before sleeping', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (2, 1, N'Oily, especially in the T-zone (nose and forehead)', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (3, 1, N'Dry and flaky', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (4, 1, N'Redness and peeling', 5)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (5, 2, N'Feels good', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (6, 2, N'Still oily', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (7, 2, N'Dry and rough', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (8, 2, N'Red and irritated', 5)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (9, 3, N'Small pores', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (10, 3, N'Large pores', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (11, 3, N'Dry appearance', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (12, 3, N'Redness', 5)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (13, 4, N'Soft and smooth', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (14, 4, N'Oily', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (15, 4, N'Slightly dry', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (16, 4, N'Thin, visible blood vessels', 5)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (17, 5, N'Same as in the morning', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (18, 5, N'Shiny and greasy', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (19, 5, N'Dry', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (20, 5, N'Sensitive', 5)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (21, 6, N'Occasionally', 1)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (22, 6, N'Frequently, especially during menstrual cycle', 3)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (23, 6, N'Never', 2)
INSERT [dbo].[skin_quiz_answers] ([id], [questionId], [answer], [skinTypeId]) VALUES (24, 6, N'Only when makeup is not applied properly', 4)
SET IDENTITY_INSERT [dbo].[skin_quiz_answers] OFF
GO
SET IDENTITY_INSERT [dbo].[skin_quiz_questions] ON 

INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (1, N'When you wake up in the morning, how does your skin feel?')
INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (2, N'Wash your face with your cleanser using warm water. After 20 - 30 minutes, how does your skin feel?')
INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (3, N'Take a close look at your pores. How do they appear?')
INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (4, N'Which of the following best describes your skin texture?')
INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (5, N'At noon, what condition is your skin in? (Do not touch, just observe in the mirror)')
INSERT [dbo].[skin_quiz_questions] ([id], [question]) VALUES (6, N'Do you often squeeze pimples?')
SET IDENTITY_INSERT [dbo].[skin_quiz_questions] OFF
GO
SET IDENTITY_INSERT [dbo].[skin_types] ON 

INSERT [dbo].[skin_types] ([id], [name], [description]) VALUES (1, N'Normal', N'Balanced skin with minimal issues')
INSERT [dbo].[skin_types] ([id], [name], [description]) VALUES (2, N'Dry', N'Skin lacks moisture and feels tight')
INSERT [dbo].[skin_types] ([id], [name], [description]) VALUES (3, N'Oily', N'Excess oil production leading to shine')
INSERT [dbo].[skin_types] ([id], [name], [description]) VALUES (4, N'Combination', N'Mix of oily and dry areas')
INSERT [dbo].[skin_types] ([id], [name], [description]) VALUES (5, N'Sensitive', N'Easily irritated and reactive skin')
SET IDENTITY_INSERT [dbo].[skin_types] OFF
GO
SET IDENTITY_INSERT [dbo].[user_skin_tests] ON 

INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (35, 1, 3, CAST(N'2025-03-21T07:32:50.603' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (36, 1, 2, CAST(N'2025-03-21T07:41:26.080' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (37, 1, 2, CAST(N'2025-03-21T07:41:37.727' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (38, 1, 2, CAST(N'2025-03-21T07:41:52.027' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (39, 1, 5, CAST(N'2025-03-21T07:42:31.827' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (40, 1, 5, CAST(N'2025-03-21T07:48:43.530' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (41, 1, 4, CAST(N'2025-03-21T07:49:04.883' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (42, 1, 4, CAST(N'2025-03-21T07:50:14.513' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (43, 1, 3, CAST(N'2025-03-21T07:50:32.850' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (44, 1, 1, CAST(N'2025-03-21T08:13:59.580' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (45, 1, 2, CAST(N'2025-03-21T08:25:53.797' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (46, 1, 4, CAST(N'2025-03-21T08:26:56.830' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (47, 1, 2, CAST(N'2025-03-21T08:47:50.497' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (48, 1, 3, CAST(N'2025-03-21T08:57:33.933' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (49, 1, 2, CAST(N'2025-03-21T08:58:18.247' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (50, 1, 2, CAST(N'2025-03-21T08:58:48.850' AS DateTime))
INSERT [dbo].[user_skin_tests] ([id], [userId], [skinTypeId], [createdAt]) VALUES (51, 1, 2, CAST(N'2025-03-21T08:59:06.180' AS DateTime))
SET IDENTITY_INSERT [dbo].[user_skin_tests] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [fullName], [email], [phone], [passwordHash], [address], [birthdate], [skinTypeId], [loyaltyPoints], [roleId], [createdAt]) VALUES (1, N'Trần Thịnh', N'thinh@gmail.com', N'012345678', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Hồ Chí Minh', NULL, 2, 644, 2, CAST(N'2025-03-16T12:18:02.840' AS DateTime))
INSERT [dbo].[users] ([id], [fullName], [email], [phone], [passwordHash], [address], [birthdate], [skinTypeId], [loyaltyPoints], [roleId], [createdAt]) VALUES (2, N'Staff Thinh', N'st@gmail', N'098765435454', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Hồ Chí Minh', NULL, 1, 0, 1, CAST(N'2025-03-19T12:38:37.510' AS DateTime))
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__categori__72E12F1BDD3A86CC]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[categories] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__discount__357D4CF93624676F]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[discounts] ADD UNIQUE NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__roles__B19478614EF16F1E]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[roles] ADD UNIQUE NONCLUSTERED 
(
	[roleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__skin_typ__72E12F1B5A3C6267]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[skin_types] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__users__AB6E61642FDFC2CE]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[users] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__users__B43B145F42C04CFE]    Script Date: 3/25/2025 5:01:57 PM ******/
ALTER TABLE [dbo].[users] ADD UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[brands] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[content] ADD  DEFAULT ((1)) FOR [isPublished]
GO
ALTER TABLE [dbo].[content] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[discounts] ADD  DEFAULT ((0)) FOR [requiredPoints]
GO
ALTER TABLE [dbo].[discounts] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[feedbacks] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[loyalty_points] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[product_details] ADD  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[product_details] ADD  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [stock]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [rating]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[user_skin_tests] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[user_vouchers] ADD  DEFAULT (getdate()) FOR [redeemedAt]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [loyaltyPoints]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[content]  WITH CHECK ADD FOREIGN KEY([authorId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[feedbacks]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[feedbacks]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[loyalty_points]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([orderId])
REFERENCES [dbo].[orders] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[product_details]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[products] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[product_skin_types]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_skin_types]  WITH CHECK ADD FOREIGN KEY([skinTypeId])
REFERENCES [dbo].[skin_types] ([id])
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD FOREIGN KEY([brandId])
REFERENCES [dbo].[brands] ([id])
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD FOREIGN KEY([categoryId])
REFERENCES [dbo].[categories] ([id])
GO
ALTER TABLE [dbo].[skin_quiz_answers]  WITH CHECK ADD FOREIGN KEY([questionId])
REFERENCES [dbo].[skin_quiz_questions] ([id])
GO
ALTER TABLE [dbo].[skin_quiz_answers]  WITH CHECK ADD FOREIGN KEY([skinTypeId])
REFERENCES [dbo].[skin_types] ([id])
GO
ALTER TABLE [dbo].[user_skin_tests]  WITH CHECK ADD FOREIGN KEY([skinTypeId])
REFERENCES [dbo].[skin_types] ([id])
GO
ALTER TABLE [dbo].[user_skin_tests]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[user_vouchers]  WITH CHECK ADD FOREIGN KEY([discountId])
REFERENCES [dbo].[discounts] ([id])
GO
ALTER TABLE [dbo].[user_vouchers]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD FOREIGN KEY([skinTypeId])
REFERENCES [dbo].[skin_types] ([id])
GO
ALTER TABLE [dbo].[content]  WITH CHECK ADD CHECK  (([contentType]='faq' OR [contentType]='news' OR [contentType]='blog'))
GO
USE [master]
GO
ALTER DATABASE [SkincareShop] SET  READ_WRITE 
GO
