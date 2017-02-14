USE [master]
GO
/****** Object:  Database [panaderia]    Script Date: 13/02/2017 07:27:39 p.m. ******/
CREATE DATABASE [panaderia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'panaderia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\panaderia.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'panaderia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\panaderia_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [panaderia] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [panaderia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [panaderia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [panaderia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [panaderia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [panaderia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [panaderia] SET ARITHABORT OFF 
GO
ALTER DATABASE [panaderia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [panaderia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [panaderia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [panaderia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [panaderia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [panaderia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [panaderia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [panaderia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [panaderia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [panaderia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [panaderia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [panaderia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [panaderia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [panaderia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [panaderia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [panaderia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [panaderia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [panaderia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [panaderia] SET  MULTI_USER 
GO
ALTER DATABASE [panaderia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [panaderia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [panaderia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [panaderia] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [panaderia] SET DELAYED_DURABILITY = DISABLED 
GO
USE [panaderia]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 13/02/2017 07:27:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[producto_id] [int] IDENTITY(1,1) NOT NULL,
	[producto_nombre] [nvarchar](50) NOT NULL,
	[producto_descripcion] [nvarchar](50) NOT NULL,
	[producto_precio] [money] NOT NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[producto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 13/02/2017 07:27:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[usuario_id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_nombre] [nvarchar](50) NOT NULL,
	[usuario_apellidos] [nvarchar](50) NOT NULL,
	[usuario_rfc] [nvarchar](15) NOT NULL,
	[usuario_direccion] [nvarchar](50) NOT NULL,
	[usuario_ciudad] [nvarchar](50) NOT NULL,
	[usuario_telefono] [nvarchar](15) NOT NULL,
	[usuario_tipo] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venta]    Script Date: 13/02/2017 07:27:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[venta_id] [int] IDENTITY(1,1) NOT NULL,
	[venta_subtotal] [money] NOT NULL,
	[venta_total] [money] NOT NULL,
	[usuario_id] [int] NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[venta_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venta_producto]    Script Date: 13/02/2017 07:27:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta_producto](
	[venta_id] [int] NOT NULL,
	[producto_id] [int] NOT NULL,
	[cantidad] [float] NOT NULL,
 CONSTRAINT [PK_venta_producto] PRIMARY KEY CLUSTERED 
(
	[venta_id] ASC,
	[producto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [fk_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [fk_usuario]
GO
ALTER TABLE [dbo].[venta_producto]  WITH CHECK ADD  CONSTRAINT [fk_producto] FOREIGN KEY([producto_id])
REFERENCES [dbo].[producto] ([producto_id])
GO
ALTER TABLE [dbo].[venta_producto] CHECK CONSTRAINT [fk_producto]
GO
ALTER TABLE [dbo].[venta_producto]  WITH CHECK ADD  CONSTRAINT [fk_venta] FOREIGN KEY([venta_id])
REFERENCES [dbo].[venta] ([venta_id])
GO
ALTER TABLE [dbo].[venta_producto] CHECK CONSTRAINT [fk_venta]
GO
USE [master]
GO
ALTER DATABASE [panaderia] SET  READ_WRITE 
GO
