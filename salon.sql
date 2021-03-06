USE [master]
GO
/****** Object:  Database [Salon]    Script Date: 01.06.2022 19:57:05 ******/
CREATE DATABASE [Salon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Salon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Salon.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Salon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Salon_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Salon] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Salon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Salon] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Salon] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Salon] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Salon] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Salon] SET ARITHABORT OFF 
GO
ALTER DATABASE [Salon] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Salon] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Salon] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Salon] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Salon] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Salon] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Salon] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Salon] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Salon] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Salon] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Salon] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Salon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Salon] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Salon] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Salon] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Salon] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Salon] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Salon] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Salon] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Salon] SET  MULTI_USER 
GO
ALTER DATABASE [Salon] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Salon] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Salon] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Salon] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Salon]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 01.06.2022 19:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [varchar](50) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[address] [varchar](max) NOT NULL,
	[regular_customer] [varchar](20) NULL,
	[Id_user] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 01.06.2022 19:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_position] [int] NOT NULL,
	[FIO] [varchar](200) NOT NULL,
	[address] [varchar](max) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[email] [varchar](50) NULL,
	[photo] [nvarchar](max) NULL,
	[Id_user] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 01.06.2022 19:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_customer] [int] NOT NULL,
	[Id_employee] [int] NOT NULL,
	[start_datetime] [datetime] NOT NULL,
	[end_datetime] [datetime] NULL,
	[grand_total] [int] NULL,
 CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntryServicesRefs]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntryServicesRefs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_entry] [int] NOT NULL,
	[Id_service] [int] NOT NULL,
 CONSTRAINT [PK_EntryServicesRefs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterRatings]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterRatings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_master] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[date] [date] NOT NULL,
 CONSTRAINT [PK_MasterRatings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Position]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Position](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[position] [varchar](50) NOT NULL,
	[salary] [varchar](20) NOT NULL,
	[schedule] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Services]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Works]    Script Date: 01.06.2022 19:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Works](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_employee] [int] NOT NULL,
	[works] [nvarchar](max) NULL,
 CONSTRAINT [PK_Works] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (1, N'Вера Александровна Виноградова ', N'81226719597', N'280811, Омская область, город Балашиха, пер. Сталина, 15', N'да', 9)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (2, N'Георгий Александрович Лукин', N'89224679383', N'508299, Кемеровская область, город Кашира, пер. Гагарина, 42', NULL, 17)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (3, N'Гавриил Сергеевич Игнатьев', N'88003173579', N'371407, Рязанская область, город Шатура, пл. Чехова, 48', NULL, 18)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (4, N'Давид Андреевич Фадеев', N'89228512323', N'738763, Курская область, город Егорьевск, спуск Чехова, 66', NULL, 19)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (5, N'Изабелла Борисовна Архиповаа', N'4954456143', N'330228, Ивановская область, город Ногинск, ул. Славы, 42', N'да', 20)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (6, N'Зинаида Борисовна Дорофеева ', N'88002325637', N'584771, Брянская область, город Раменское, наб. Славы, 53', N'да', 21)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (7, N'Давид Андреевич Фадеев', N'8128239342', N'310403, Кировская область, город Солнечногорск, пл. Балканская, 76', N'да', 22)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (8, N'Виктор Иванович Молчанов', N'88005823919', N'180288, Тверская область, город Одинцово, ул. Бухарестская, 37', NULL, 23)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (9, N'Екатерина Сергеевна Бобылёва', N'88005114361', N'606703, Амурская область, город Чехов, пл. Будапештсткая, 91', NULL, 24)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (10, N'Виктория Сергеевна Лазарева ', N'4958937117', N'861543, Томская область, город Истра, бульвар Славы, 42', NULL, 25)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (11, N'Михаил Андреевич Носов ', N'35222734419', N'946112, Волгоградская область, город Шаховская, пл. Сталина, 98', NULL, 26)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (12, N'Люся Владимировна Фёдорова', N'89222332768', N'426305, Калининградская область, город Чехов, пл. Ломоносова, 00', N'да', 27)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (13, N'Сава Александровна Титова', N'4956882859', N'434616, Калининградская область, город Павловский Посад, пл. Ладыгина, 83', NULL, 28)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (14, N'Анжелика Дмитриевна Горбунова', N'35222392895', N'928260, Нижегородская область, город Балашиха, пл. Косиора, 44', NULL, 29)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (15, N'Таисия Андреевна Кондратьева ', N'4952846937', N'599158, Ростовская область, город Озёры, ул. Космонавтов, 05', NULL, 30)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (16, N'Ксения Андреевна Михайлова', N'88007527145', N'210024, Белгородская область, город Сергиев Посад, наб. Ломоносова, 43', NULL, 31)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (17, N'Карпов Иосиф Максимович', N'4953742179', N'491360, Московская область, город Одинцово, въезд Ленина, 19', NULL, 32)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (18, N'Белова Полина Владимировна', N'89229471199', N'747695, Амурская область, город Сергиев Посад, въезд Бухарестская, 46', NULL, 33)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (19, N'Злата Романовна Корнилова', N'88007532768', N'357783, Рязанская область, город Павловский Посад, наб. Домодедовская, 44', NULL, 34)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (20, N'Клавдия Фёдоровна Кудряшова', N'4952777759', N'294596, Мурманская область, город Шаховская, пр. Домодедовская, 88', NULL, 35)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (21, N'Шубина Валерия Евгеньевна', N'88009334875', N'116028, Челябинская область, город Балашиха, шоссе Космонавтов, 69', NULL, 36)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (22, N'Нонна Владимировна Горбунова', N'35222418198', N'437208, Астраханская область, город Озёры, спуск Славы, 45', NULL, 37)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (23, N'Вера Фёдоровна Максимова', N'4951786132', N'038182, Курганская область, город Москва, спуск Космонавтов, 16', NULL, 38)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (24, N'Федосья Борисовна Мельникова', N'89223496796', N'243999, Орловская область, город Можайск, пер. Домодедовская, 77', NULL, 39)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (25, N'Зоя Романовна Селезнёва', N'4958113655', N'964386, Оренбургская область, город Чехов, пл. Косиора, 80', N'да', 40)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (26, N'Регина Львовна Тимофеева', N'35222869461', N'352034, Сахалинская область, город Домодедово, пр. Балканская, 97', N'да', 41)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (27, N'Валерия Владимировна Хохлова', N'4956837657', N'532703, Пензенская область, город Чехов, наб. Чехова, 81', NULL, 42)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (28, N'Эмилия Андреевна Елисеева', N'89229163431', N'661101, Оренбургская область, город Волоколамск, въезд Чехова, 16', NULL, 43)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (29, N'София Алексеевна Мухина', N'89226359719', N'521087, Орловская область, город Егорьевск, шоссе Ладыгина, 14', NULL, 44)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (30, N'Жанна Евгеньевна Гришинаа', N'4956985686', N'837800, Псковская область, город Солнечногорск, ул. Сталина, 67', NULL, 45)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (31, N'Константин Максимович Панфилов ', N'88002976948', N'585758, Самарская область, город Красногорск, бульвар Балканская, 13', NULL, 46)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (32, N'Лада Дмитриевна Зайцева ', N'4954844265', N'225511, Новосибирская область, город Можайск, наб. Ладыгина, 82', N'да', 47)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (33, N'Вера Андреевна Назарова ', N'89224373891', N'136886,  Амурская  область, город Видное,  въезд Космонавтов,  39', NULL, 48)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (34, N'Ева Борисовна Беспалова', N'35222774644', N'254238, Нижегородская область, город Павловский Посад, проезд Балканская, 23', NULL, 49)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (35, N'Ярослава Фёдоровна Медведева', N'8127661188', N'449723, Смоленская область, город Наро-Фоминск, пер. Ломоносова, 94', N'да', 50)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (36, N'Иван Евгеньевич Белоусова', N'8121318424', N'021293, Амурская область, город Наро-Фоминск, шоссе Славы, 40', N'да', 51)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (37, N'Яна Дмитриевна Моисееваа', N'35222897497', N'152450, Брянская область, город Серебряные Пруды, наб. 1905 года, 56', NULL, 52)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (38, N'Виктория Романовна Королёваа', N'8127317952', N'063695, Новгородская область, город Можайск, шоссе Гагарина, 39', NULL, 53)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (39, N'Злата Сергеевна Архипова', N'4957443711', N'899084, Амурская область, город Талдом, спуск Балканская, 34', NULL, 54)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (40, N'Флорентина Фёдоровна Игнатьева', N'35222285934', N'975616, Вологодская область, город Клин, пер. Косиора, 57', N'да', 55)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (41, N'Фаина Львовна Степанова ', N'88004868447', N'990758, Челябинская область, город Серпухов, въезд Космонавтов, 57', NULL, 56)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (42, N'Люся Дмитриевна Лыткина', N'4959717124', N'546530, Тульская область, город Видное, наб. Гагарина, 63', NULL, 57)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (43, N'Ева Романовна Беспалова ', N'88003599529', N'239247, Архангельская область, город Лотошино, бульвар Ломоносова, 28', NULL, 58)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (44, N'Вера Евгеньевна Денисоваа', N'8128875997', N'479565, Курганская область, город Клин, пл. Ленина, 54', NULL, 59)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (45, N'Инесса Сергеевна Чернова ', N'88007656746', N'475644, Ивановская область, город Луховицы, въезд Будапештсткая, 43', NULL, 60)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (46, N'Любовь Ивановна Сидорова ', N'4956768198', N'304975, Пензенская область, город Солнечногорск, шоссе Балканская, 76', NULL, 61)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (47, N'Зоя Андреевна Соболева', N'89224972219', N'880551, Ленинградская область, город Красногорск, ул. Гоголя, 61', N'да', 62)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (48, N'Елена Дмитриевна Шарапова ', N'8122644175', N'966815, Новгородская область, город Одинцово, пр. Космонавтов, 19', NULL, 63)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (49, N'Фоминаа Лариса Романовна', N'89221678939', N'016215, Воронежская область, город Зарайск, ул. Косиора, 48', NULL, 64)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (50, N'Нонна Фёдоровна Федотова', N'89228527343', N'318328, Кемеровская область, город Павловский Посад, спуск 1905 года, 92', N'да', 65)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (51, N'Фаина Фёдоровна Филиппова', N'35222615874', N'265653, Калужская область, город Ступино, шоссе Гоголя, 89', NULL, 66)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (52, N'Альбина Александровна Романова', N'8121116711', N'373761, Псковская область, город Наро-Фоминск, наб. Гагарина, 03', N'да', 67)
INSERT [dbo].[Customer] ([Id], [FIO], [phone], [address], [regular_customer], [Id_user]) VALUES (59, N'Семенова Дарья Игоревна', N'89197797660', N'г. Зарайск, 1 мкр-н д.7 кв.3', NULL, 16)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (1, 1, N'Алина Борисовна Потапова', N'035823, Смоленская область, город Одинцово, проезд Будапештсткая, 61', N'89224679383', N'viktoriy.belozerova@isaev.net', N'\masters\variant1.jpg', 1)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (2, 2, N'Люся Владимировна Фёдорова', N'426305, Калининградская область, город Чехов, пл. Ломоносова, 00', N'89222332768', N'wsamsonov@martynov.ru', N'\masters\variant2.jpg', 2)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (3, 2, N'Анжелика Дмитриевна Горбунова', N'928260, Нижегородская область, город Балашиха, пл. Косиора, 44', N'89225961453', N'galina31@melnikov.ru', N'\masters\variant3.jpg', 3)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (4, 2, N'Ксения Андреевна Михайлова', N'210024, Белгородская область, город Сергиев Посад, наб. Ломоносова, 43', N'88007527145', N'email: gorskov.larisa@kalinin.com', N'\masters\variant4.jpg', 4)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (5, 2, N'Нонна Владимировна Горбунова', N'437208, Астраханская область, город Озёры, спуск Славы, 45', N'89522418198', N'ybragina@odintov.org', N'\masters\variant5.jpg', 5)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (6, 2, N'Зоя Романовна Селезнёва', N'964386, Оренбургская область, город Чехов, пл. Косиора, 80', N'89326279625', N'veronika.egorov@bespalova.com', N'\masters\variant6.jpg', 6)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (7, 2, N'Злата Романовна Корнилова', N'357783, Рязанская область, город Павловский Посад, наб. Домодедовская, 44', N'89786359719', N'danila.birykov@stepanov.ru', N'\masters\variant7.jpg', 7)
INSERT [dbo].[Employee] ([Id], [Id_position], [FIO], [address], [phone], [email], [photo], [Id_user]) VALUES (8, 2, N'София Алексеевна Мухина', N'89228771228', N'89541265474', N'liliy62@grisina.ru', N'\masters\variant8.jpg', 8)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[Entries] ON 

INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (1, 1, 2, CAST(0x0000AE7700B54640 AS DateTime), CAST(0x0000AE7700D63BC0 AS DateTime), 1200)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (2, 2, 2, CAST(0x0000AE7700DE7920 AS DateTime), CAST(0x0000AE7700FF6EA0 AS DateTime), 1200)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (4, 1, 2, CAST(0x0000AE8C00C5C100 AS DateTime), CAST(0x0000AE8B00D63BC0 AS DateTime), 950)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (5, 1, 2, CAST(0x0000AE8E0020F580 AS DateTime), CAST(0x0000AE8B0083D600 AS DateTime), 500)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (6, 1, 2, CAST(0x0000AE8B008C1360 AS DateTime), CAST(0x0000AE8B0020F580 AS DateTime), 400)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (7, 1, 2, CAST(0x0000AE8B0083D600 AS DateTime), CAST(0x0000AE8B00A4CB80 AS DateTime), 1000)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (8, 1, 2, CAST(0x0000AE8B00B54640 AS DateTime), CAST(0x0000AE9200D63E18 AS DateTime), 1150)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (9, 1, 2, CAST(0x0000AE8B00D63BC0 AS DateTime), CAST(0x0000AE8B00F73140 AS DateTime), 900)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (12, 1, 2, CAST(0x0000AEA10083D600 AS DateTime), CAST(0x0000AEA100C1CDAC AS DateTime), 1900)
INSERT [dbo].[Entries] ([Id], [Id_customer], [Id_employee], [start_datetime], [end_datetime], [grand_total]) VALUES (13, 59, 7, CAST(0x0000AEA100E6B680 AS DateTime), CAST(0x0000AEA10107DAE0 AS DateTime), 1800)
SET IDENTITY_INSERT [dbo].[Entries] OFF
SET IDENTITY_INSERT [dbo].[EntryServicesRefs] ON 

INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (1, 1, 3)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (2, 1, 5)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (3, 2, 4)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (4, 2, 6)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (5, 4, 2)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (6, 5, 10)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (7, 6, 11)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (8, 7, 1)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (9, 7, 5)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (10, 8, 4)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (11, 8, 7)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (12, 9, 10)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (13, 9, 11)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (14, 12, 1)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (15, 12, 4)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (16, 13, 1)
INSERT [dbo].[EntryServicesRefs] ([Id], [Id_entry], [Id_service]) VALUES (17, 13, 3)
SET IDENTITY_INSERT [dbo].[EntryServicesRefs] OFF
SET IDENTITY_INSERT [dbo].[MasterRatings] ON 

INSERT [dbo].[MasterRatings] ([Id], [Id_master], [rating], [date]) VALUES (1, 2, 5, CAST(0xED430B00 AS Date))
INSERT [dbo].[MasterRatings] ([Id], [Id_master], [rating], [date]) VALUES (2, 2, 3, CAST(0xED430B00 AS Date))
INSERT [dbo].[MasterRatings] ([Id], [Id_master], [rating], [date]) VALUES (3, 3, 5, CAST(0xFC430B00 AS Date))
INSERT [dbo].[MasterRatings] ([Id], [Id_master], [rating], [date]) VALUES (4, 7, 4, CAST(0xFC430B00 AS Date))
SET IDENTITY_INSERT [dbo].[MasterRatings] OFF
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([Id], [position], [salary], [schedule]) VALUES (1, N'Администратор', N'25000', N'5/2')
INSERT [dbo].[Position] ([Id], [position], [salary], [schedule]) VALUES (2, N'Мастер', N'35000', N'2/2')
INSERT [dbo].[Position] ([Id], [position], [salary], [schedule]) VALUES (3, N'Уборщица', N'15000', N'5/2')
SET IDENTITY_INSERT [dbo].[Position] OFF
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (1, N'Комбинированный маникюр', 800)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (2, N'Комбинированный педикюр ', 950)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (3, N'Однотонный маникюр', 1000)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (4, N'Однотонный педикюр', 1100)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (5, N'Френч', 200)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (6, N'Легкий дизайн', 100)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (7, N'Ручная роспись(за 1 ноготок)', 150)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (8, N'Ремонт трещины', 50)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (9, N'Наращивание 1 ноготка', 100)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (10, N'Снятие без дальнейшего покрытия', 500)
INSERT [dbo].[Services] ([Id], [name], [price]) VALUES (11, N'Холодная парафинотерапия', 400)
SET IDENTITY_INSERT [dbo].[Services] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (1, N'Admin', N'157086', 2)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (2, N'Master1', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (3, N'Master2', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (4, N'Master3', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (5, N'Master4', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (6, N'Master5', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (7, N'Master6', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (8, N'Master7', N'123', 1)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (9, N'Customer1', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (16, N'Daria21', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (17, N'Customer2', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (18, N'Customer3', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (19, N'Customer4', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (20, N'Customer5', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (21, N'Customer6', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (22, N'Customer7', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (23, N'Customer8', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (24, N'Customer9', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (25, N'Customer10', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (26, N'Customer11', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (27, N'Customer12', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (28, N'Customer13', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (29, N'Customer14', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (30, N'Customer15', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (31, N'Customer16', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (32, N'Customer17', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (33, N'Customer18', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (34, N'Customer19', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (35, N'Customer20', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (36, N'Customer21', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (37, N'Customer22', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (38, N'Customer23', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (39, N'Customer24', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (40, N'Customer25', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (41, N'Customer26', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (42, N'Customer27', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (43, N'Customer28', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (44, N'Customer29', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (45, N'Customer30', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (46, N'Customer31', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (47, N'Customer32', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (48, N'Customer33', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (49, N'Customer34', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (50, N'Customer35', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (51, N'Customer36', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (52, N'Customer37', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (53, N'Customer38', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (54, N'Customer39', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (55, N'Customer40', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (56, N'Customer41', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (57, N'Customer42', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (58, N'Customer43', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (59, N'Customer44', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (60, N'Customer45', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (61, N'Customer46', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (62, N'Customer47', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (63, N'Customer48', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (64, N'Customer49', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (65, N'Customer50', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (66, N'Customer51', N'321', 3)
INSERT [dbo].[User] ([Id], [login], [password], [role]) VALUES (67, N'Customer52', N'321', 3)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Works] ON 

INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (1, 2, N'\works\manikur1.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (2, 2, N'\works\manikur2.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (3, 2, N'\works\manikur3.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (4, 2, N'\works\manikur4.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (5, 2, N'\works\manikur5.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (6, 3, N'\works\manikur6.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (7, 3, N'\works\manikur7.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (8, 3, N'\works\manikur8.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (9, 3, N'\works\manikur9.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (10, 3, N'\works\manikur10.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (11, 4, N'\works\manikur11.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (12, 4, N'\works\manikur12.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (13, 4, N'\works\manikur13.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (14, 4, N'\works\manikur14.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (15, 4, N'\works\manikur15.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (16, 5, N'\works\manikur16.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (17, 5, N'\works\manikur17.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (18, 5, N'\works\manikur18.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (19, 5, N'\works\manikur19.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (20, 5, N'\works\manikur20.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (21, 5, N'\works\manikur21.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (22, 6, N'\works\manikur22.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (23, 6, N'\works\manikur23.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (24, 6, N'\works\manikur24.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (25, 6, N'\works\manikur25.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (26, 6, N'\works\manikur26.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (27, 7, N'\works\manikur27.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (28, 7, N'\works\manikur28.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (29, 7, N'\works\manikur29.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (30, 7, N'\works\manikur30.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (31, 7, N'\works\manikur31.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (32, 8, N'\works\manikur32.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (33, 8, N'\works\manikur33.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (34, 8, N'\works\manikur34.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (35, 8, N'\works\manikur35.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (36, 8, N'\works\manikur36.jpg')
INSERT [dbo].[Works] ([Id], [Id_employee], [works]) VALUES (37, 8, N'\works\manikur37.jpg')
SET IDENTITY_INSERT [dbo].[Works] OFF
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([Id_user])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_User]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Position] FOREIGN KEY([Id_position])
REFERENCES [dbo].[Position] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Position]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([Id_user])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User]
GO
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_Entries_Customer] FOREIGN KEY([Id_customer])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_Entries_Customer]
GO
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_Entries_Employee] FOREIGN KEY([Id_employee])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_Entries_Employee]
GO
ALTER TABLE [dbo].[EntryServicesRefs]  WITH CHECK ADD  CONSTRAINT [FK_EntryServicesRefs_Entries] FOREIGN KEY([Id_entry])
REFERENCES [dbo].[Entries] ([Id])
GO
ALTER TABLE [dbo].[EntryServicesRefs] CHECK CONSTRAINT [FK_EntryServicesRefs_Entries]
GO
ALTER TABLE [dbo].[EntryServicesRefs]  WITH CHECK ADD  CONSTRAINT [FK_EntryServicesRefs_Services] FOREIGN KEY([Id_service])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[EntryServicesRefs] CHECK CONSTRAINT [FK_EntryServicesRefs_Services]
GO
ALTER TABLE [dbo].[MasterRatings]  WITH CHECK ADD  CONSTRAINT [FK_MasterRatings_Employee] FOREIGN KEY([Id_master])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[MasterRatings] CHECK CONSTRAINT [FK_MasterRatings_Employee]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_Employee] FOREIGN KEY([Id_employee])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_Employee]
GO
USE [master]
GO
ALTER DATABASE [Salon] SET  READ_WRITE 
GO
