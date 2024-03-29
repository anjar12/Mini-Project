USE [master]
GO
/****** Object:  Database [Actuator]    Script Date: 19/01/2024 22:22:41 ******/
CREATE DATABASE [Actuator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Actuator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Actuator.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Actuator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Actuator_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Actuator] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Actuator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Actuator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Actuator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Actuator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Actuator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Actuator] SET ARITHABORT OFF 
GO
ALTER DATABASE [Actuator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Actuator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Actuator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Actuator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Actuator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Actuator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Actuator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Actuator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Actuator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Actuator] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Actuator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Actuator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Actuator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Actuator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Actuator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Actuator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Actuator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Actuator] SET RECOVERY FULL 
GO
ALTER DATABASE [Actuator] SET  MULTI_USER 
GO
ALTER DATABASE [Actuator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Actuator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Actuator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Actuator] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Actuator] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Actuator] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Actuator', N'ON'
GO
ALTER DATABASE [Actuator] SET QUERY_STORE = OFF
GO
USE [Actuator]
GO
/****** Object:  Table [dbo].[ms_storage_location]    Script Date: 19/01/2024 22:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ms_storage_location](
	[Location_id] [varchar](10) NOT NULL,
	[location_name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TESTGUID] PRIMARY KEY CLUSTERED 
(
	[Location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ms_user]    Script Date: 19/01/2024 22:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ms_user](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](20) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_ms_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tr_bpkb]    Script Date: 19/01/2024 22:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_bpkb](
	[agreement_number] [varchar](100) NOT NULL,
	[bpkb_no] [varchar](100) NOT NULL,
	[branch_id] [varchar](20) NOT NULL,
	[faktur_no] [varchar](20) NOT NULL,
	[faktur_date] [datetime] NOT NULL,
	[location_id] [varchar](10) NOT NULL,
	[police_no] [varchar](20) NOT NULL,
	[bpkb_date] [datetime] NOT NULL,
	[bpkb_date_in] [datetime] NOT NULL,
	[created_by] [varchar](20) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[last_updated_by] [varchar](20) NULL,
	[last_updated_on] [datetime] NULL,
 CONSTRAINT [PK_BKPB] PRIMARY KEY CLUSTERED 
(
	[agreement_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ms_storage_location] ([Location_id], [location_name]) VALUES (N'68F09006-2', N'BOGOR')
INSERT [dbo].[ms_storage_location] ([Location_id], [location_name]) VALUES (N'LOK0001', N'JAKARTA')
INSERT [dbo].[ms_storage_location] ([Location_id], [location_name]) VALUES (N'LOK0002', N'BANDUNG')
INSERT [dbo].[ms_storage_location] ([Location_id], [location_name]) VALUES (N'LOK0003', N'BOGOR')
INSERT [dbo].[ms_storage_location] ([Location_id], [location_name]) VALUES (N'LOK0004', N'DEPOK')
GO
SET IDENTITY_INSERT [dbo].[ms_user] ON 

INSERT [dbo].[ms_user] ([user_id], [user_name], [password], [is_active]) VALUES (1, N'Admin', N'Pass1234', 1)
SET IDENTITY_INSERT [dbo].[ms_user] OFF
GO
ALTER TABLE [dbo].[ms_storage_location] ADD  CONSTRAINT [DF_ms_storage_location_Location_id]  DEFAULT (left(newid(),(10))) FOR [Location_id]
GO
ALTER TABLE [dbo].[tr_bpkb]  WITH CHECK ADD  CONSTRAINT [FK_tr_bpkb_ms_storage_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[ms_storage_location] ([Location_id])
GO
ALTER TABLE [dbo].[tr_bpkb] CHECK CONSTRAINT [FK_tr_bpkb_ms_storage_location]
GO
USE [master]
GO
ALTER DATABASE [Actuator] SET  READ_WRITE 
GO
