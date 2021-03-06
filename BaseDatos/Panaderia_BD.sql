USE [master]
GO
/****** Object:  Database [Panaderia_2]    Script Date: 18/03/2017 04:51:33 p. m. ******/
CREATE DATABASE [Panaderia_2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Panaderia_2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Panaderia_2.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Panaderia_2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Panaderia_2_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Panaderia_2] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Panaderia_2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Panaderia_2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Panaderia_2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Panaderia_2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Panaderia_2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Panaderia_2] SET ARITHABORT OFF 
GO
ALTER DATABASE [Panaderia_2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Panaderia_2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Panaderia_2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Panaderia_2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Panaderia_2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Panaderia_2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Panaderia_2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Panaderia_2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Panaderia_2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Panaderia_2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Panaderia_2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Panaderia_2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Panaderia_2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Panaderia_2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Panaderia_2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Panaderia_2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Panaderia_2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Panaderia_2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Panaderia_2] SET  MULTI_USER 
GO
ALTER DATABASE [Panaderia_2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Panaderia_2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Panaderia_2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Panaderia_2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Panaderia_2] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Panaderia_2]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 18/03/2017 04:51:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[producto_id] [int] IDENTITY(1,1) NOT NULL,
	[producto_nombre] [varchar](50) NOT NULL,
	[producto_descripcion] [varchar](50) NOT NULL,
	[producto_precio] [money] NOT NULL,
	[producto_activo] [bit] NOT NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[producto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 18/03/2017 04:51:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[usuario_id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_nombre] [varchar](50) NOT NULL,
	[usuario_apellidos] [varchar](50) NOT NULL,
	[usuario_rfc] [varchar](50) NOT NULL,
	[usuario_direccion] [varchar](50) NOT NULL,
	[usuario_ciudad] [varchar](50) NOT NULL,
	[usuario_telefono] [varchar](20) NOT NULL,
	[usuario_tipo] [varchar](20) NOT NULL,
	[usuario_contrasena] [varchar](50) NOT NULL,
	[usuario_activo] [int] NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venta]    Script Date: 18/03/2017 04:51:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[venta_id] [int] IDENTITY(1,1) NOT NULL,
	[venta_subtotal] [money] NOT NULL,
	[venta_total] [money] NOT NULL,
	[usuario_id] [int] NOT NULL,
	[venta_fecha_hora] [date] NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[venta_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venta_producto]    Script Date: 18/03/2017 04:51:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta_producto](
	[venta_id] [int] IDENTITY(1,1) NOT NULL,
	[producto_id] [int] NOT NULL,
	[cantidad] [float] NOT NULL,
 CONSTRAINT [PK_venta_producto] PRIMARY KEY CLUSTERED 
(
	[venta_id] ASC,
	[producto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[lista_productos]    Script Date: 18/03/2017 04:51:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[lista_productos] AS 
	SELECT producto.producto_id, 
		   producto.producto_nombre, 
		   producto.producto_descripcion, 
		   producto.producto_precio, 
		   venta_producto.venta_id, 
		   venta_producto.cantidad 
	FROM producto 
	JOIN venta_producto ON producto.producto_id = venta_producto.producto_id;
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_usuario]
GO
USE [master]
GO
ALTER DATABASE [Panaderia_2] SET  READ_WRITE 
GO
