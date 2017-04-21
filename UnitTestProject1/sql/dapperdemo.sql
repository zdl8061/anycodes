USE [master]
GO
/****** Object:  Database [DapperDemo]    Script Date: 2017/4/21 星期五 下午 15:20:30 ******/
CREATE DATABASE [DapperDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DapperDemo', FILENAME = N'F:\Win8DATA\DapperDemo.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DapperDemo_log', FILENAME = N'F:\Win8DATA\DapperDemo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DapperDemo] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DapperDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DapperDemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DapperDemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DapperDemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DapperDemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DapperDemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [DapperDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DapperDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DapperDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DapperDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DapperDemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DapperDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DapperDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DapperDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DapperDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DapperDemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DapperDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DapperDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DapperDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DapperDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DapperDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DapperDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DapperDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DapperDemo] SET RECOVERY FULL 
GO
ALTER DATABASE [DapperDemo] SET  MULTI_USER 
GO
ALTER DATABASE [DapperDemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DapperDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DapperDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DapperDemo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DapperDemo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DapperDemo', N'ON'
GO
USE [DapperDemo]
GO
/****** Object:  Table [dbo].[CICRole]    Script Date: 2017/4/21 星期五 下午 15:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CICRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CICUser]    Script Date: 2017/4/21 星期五 下午 15:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CICUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](500) NULL,
	[Email] [nvarchar](256) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[IsFirstTimeLogin] [bit] NOT NULL DEFAULT ((1)),
	[AccessFailedCount] [int] NOT NULL DEFAULT ((0)),
	[CreationDate] [datetime] NOT NULL DEFAULT (getdate()),
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CICUserRole]    Script Date: 2017/4/21 星期五 下午 15:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CICUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CICRole] ON 

GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (1, N'添加')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (2, N'修改')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (3, N'删除')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (4, N'添加2')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (5, N'修改2')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (6, N'删除2')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (7, N'添加3')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (8, N'修改3')
GO
INSERT [dbo].[CICRole] ([RoleId], [RoleName]) VALUES (9, N'删除3')
GO
SET IDENTITY_INSERT [dbo].[CICRole] OFF
GO
SET IDENTITY_INSERT [dbo].[CICUser] ON 

GO
INSERT [dbo].[CICUser] ([UserId], [Username], [PasswordHash], [Email], [PhoneNumber], [IsFirstTimeLogin], [AccessFailedCount], [CreationDate], [IsActive]) VALUES (1, N'张三', N'123123', N'zhangsan@sina.com', N'13656549871', 1, 2, CAST(N'2017-04-21 15:02:42.383' AS DateTime), 1)
GO
INSERT [dbo].[CICUser] ([UserId], [Username], [PasswordHash], [Email], [PhoneNumber], [IsFirstTimeLogin], [AccessFailedCount], [CreationDate], [IsActive]) VALUES (2, N'李四', N'fffff44', N'lisi@sina.com', N'15632165412', 0, 3, CAST(N'2017-04-21 15:02:57.620' AS DateTime), 1)
GO
INSERT [dbo].[CICUser] ([UserId], [Username], [PasswordHash], [Email], [PhoneNumber], [IsFirstTimeLogin], [AccessFailedCount], [CreationDate], [IsActive]) VALUES (3, N'王五', N'12351515', N'wangwu@sina.com', N'18926321521', 1, 5, CAST(N'2017-04-21 15:03:00.457' AS DateTime), 0)
GO
INSERT [dbo].[CICUser] ([UserId], [Username], [PasswordHash], [Email], [PhoneNumber], [IsFirstTimeLogin], [AccessFailedCount], [CreationDate], [IsActive]) VALUES (4, N'赵六', N'1845421', N'zhaoliu@sina.com', N'17754865412', 1, 1, CAST(N'2017-04-21 15:03:03.707' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[CICUser] OFF
GO
SET IDENTITY_INSERT [dbo].[CICUserRole] ON 

GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (4, 2, 4)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (5, 3, 4)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (6, 3, 5)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (7, 4, 6)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (8, 4, 7)
GO
INSERT [dbo].[CICUserRole] ([Id], [UserId], [RoleId]) VALUES (9, 4, 8)
GO
SET IDENTITY_INSERT [dbo].[CICUserRole] OFF
GO
ALTER TABLE [dbo].[CICUserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[CICRole] ([RoleId])
GO
ALTER TABLE [dbo].[CICUserRole]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[CICUser] ([UserId])
GO
USE [master]
GO
ALTER DATABASE [DapperDemo] SET  READ_WRITE 
GO
