USE [master]
GO
/****** Object:  Database [SONACO]    Script Date: 31/10/2024 14:35:00 ******/
CREATE DATABASE [SONACO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SONACO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLSRV_16\MSSQL\DATA\SONACO.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SONACO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLSRV_16\MSSQL\DATA\SONACO_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SONACO] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SONACO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SONACO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SONACO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SONACO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SONACO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SONACO] SET ARITHABORT OFF 
GO
ALTER DATABASE [SONACO] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SONACO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SONACO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SONACO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SONACO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SONACO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SONACO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SONACO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SONACO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SONACO] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SONACO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SONACO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SONACO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SONACO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SONACO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SONACO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SONACO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SONACO] SET RECOVERY FULL 
GO
ALTER DATABASE [SONACO] SET  MULTI_USER 
GO
ALTER DATABASE [SONACO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SONACO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SONACO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SONACO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SONACO] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SONACO', N'ON'
GO
ALTER DATABASE [SONACO] SET QUERY_STORE = OFF
GO
USE [SONACO]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [SONACO]
GO
/****** Object:  Table [dbo].[Entree]    Script Date: 31/10/2024 14:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entree](
	[NumEntree] [varchar](20) NOT NULL,
	[DateArr] [date] NULL,
	[CodeFour] [nchar](10) NULL,
	[CodeClient] [nchar](10) NULL,
	[NbRouleau] [int] NULL,
	[Largeur] [real] NULL,
 CONSTRAINT [PK_Entree] PRIMARY KEY CLUSTERED 
(
	[NumEntree] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lots]    Script Date: 31/10/2024 14:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lots](
	[NumLot] [varchar](10) NOT NULL,
	[LotNumEntree] [varchar](10) NULL,
	[LotDateProd] [date] NULL,
	[LotClient] [nchar](10) NULL,
	[LotFournisseur] [nchar](10) NULL,
	[LotOperateurEnr] [nchar](10) NULL,
	[LotDebutEnr] [time](7) NULL,
 CONSTRAINT [PK_Lots] PRIMARY KEY CLUSTERED 
(
	[NumLot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rouleau]    Script Date: 31/10/2024 14:35:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rouleau](
	[RouNumLot] [varchar](20) NOT NULL,
	[RouNumRouleau] [int] NOT NULL,
	[RouPoidsNet] [real] NULL,
	[RouMetrage] [real] NULL,
	[RouNumEnsouple] [int] NULL,
 CONSTRAINT [PK_Rouleau] PRIMARY KEY CLUSTERED 
(
	[RouNumLot] ASC,
	[RouNumRouleau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [SONACO] SET  READ_WRITE 
GO
