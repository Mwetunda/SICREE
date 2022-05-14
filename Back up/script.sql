USE [master]
GO
/****** Object:  Database [SICREE]    Script Date: 06/04/2022 20:48:39 ******/
CREATE DATABASE [SICREE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SICREE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SICREE.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SICREE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SICREE_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SICREE] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SICREE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SICREE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SICREE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SICREE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SICREE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SICREE] SET ARITHABORT OFF 
GO
ALTER DATABASE [SICREE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SICREE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SICREE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SICREE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SICREE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SICREE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SICREE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SICREE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SICREE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SICREE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SICREE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SICREE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SICREE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SICREE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SICREE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SICREE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SICREE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SICREE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SICREE] SET  MULTI_USER 
GO
ALTER DATABASE [SICREE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SICREE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SICREE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SICREE] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SICREE] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SICREE]
GO
/****** Object:  Table [dbo].[TbActa]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbActa](
	[AssembleiaID] [int] NOT NULL,
	[UsuarioID] [int] NULL,
	[QtdMesa] [int] NULL,
	[VotosBrancos] [int] NULL,
	[VotosNulos] [int] NULL,
	[VotosReclamados] [int] NULL,
	[VotosValidos] [int] NULL,
 CONSTRAINT [PK_TbActa] PRIMARY KEY CLUSTERED 
(
	[AssembleiaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TbAssembleia]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbAssembleia](
	[NumeroAssembleia] [int] NOT NULL,
	[MunicipioID] [int] NULL,
	[Endereco] [varchar](70) NULL,
 CONSTRAINT [PK_TbAssembleia] PRIMARY KEY CLUSTERED 
(
	[NumeroAssembleia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbConcorrente]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbConcorrente](
	[NumeroOrdem] [int] NOT NULL,
	[NomePartido] [varchar](100) NOT NULL,
	[Sigla] [varchar](10) NOT NULL,
	[NomePresidente] [varchar](50) NOT NULL,
	[BandeiraPartido] [varbinary](max) NOT NULL,
	[FotoPresidente] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_TbConcorrente] PRIMARY KEY CLUSTERED 
(
	[NumeroOrdem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbGeral]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbGeral](
	[ProvinciaID] [int] NOT NULL,
	[QuantidadeAssembleias] [int] NULL,
	[QuantidadeEleitores] [int] NULL,
	[QuantidadeMesasVotos] [int] NULL,
 CONSTRAINT [PK_TbGeral] PRIMARY KEY CLUSTERED 
(
	[ProvinciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TbMunicipio]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbMunicipio](
	[MunicipioID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinciaID] [int] NULL,
	[Designacao] [varchar](20) NULL,
 CONSTRAINT [PK_TbMunicipio] PRIMARY KEY CLUSTERED 
(
	[MunicipioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbProvincia]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbProvincia](
	[ProvinciaID] [int] IDENTITY(1,1) NOT NULL,
	[Designacao] [varchar](15) NULL,
 CONSTRAINT [PK_TbProvincia] PRIMARY KEY CLUSTERED 
(
	[ProvinciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbResultadoConcorrente]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbResultadoConcorrente](
	[ResultadoID] [int] IDENTITY(1,1) NOT NULL,
	[ConcorrenteID] [int] NOT NULL,
	[NumeroVotos] [int] NOT NULL,
	[AssembleiaID] [int] NULL,
 CONSTRAINT [PK_TbResultadoConcorrente] PRIMARY KEY CLUSTERED 
(
	[ResultadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TbUsuario]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbUsuario](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Login] [varchar](20) NULL,
	[Senha] [varchar](10) NULL,
	[Telefone] [varchar](10) NULL,
	[Estado] [bit] NULL,
	[Previlegio] [varchar](15) NULL,
 CONSTRAINT [PK_TbUsuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ViewActa]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewActa]
AS
SELECT        dbo.TbAssembleia.NumeroAssembleia, dbo.TbAssembleia.Endereco, dbo.TbUsuario.Nome AS Usuario, dbo.TbUsuario.UsuarioID, dbo.TbMunicipio.MunicipioID, dbo.TbMunicipio.Designacao AS Municipio, 
                         dbo.TbProvincia.ProvinciaID, dbo.TbProvincia.Designacao AS Provincia, dbo.TbActa.QtdMesa, dbo.TbActa.VotosBrancos, dbo.TbActa.VotosNulos, dbo.TbActa.VotosReclamados, dbo.TbActa.VotosValidos
FROM            dbo.TbActa INNER JOIN
                         dbo.TbAssembleia ON dbo.TbActa.AssembleiaID = dbo.TbAssembleia.NumeroAssembleia INNER JOIN
                         dbo.TbMunicipio ON dbo.TbAssembleia.MunicipioID = dbo.TbMunicipio.MunicipioID INNER JOIN
                         dbo.TbProvincia ON dbo.TbMunicipio.ProvinciaID = dbo.TbProvincia.ProvinciaID INNER JOIN
                         dbo.TbUsuario ON dbo.TbActa.UsuarioID = dbo.TbUsuario.UsuarioID

GO
/****** Object:  View [dbo].[ViewAssembleia]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewAssembleia]
AS
SELECT        dbo.TbAssembleia.NumeroAssembleia, dbo.TbAssembleia.Endereco, dbo.TbMunicipio.Designacao AS Municipio, dbo.TbProvincia.Designacao AS Provincia, dbo.TbMunicipio.MunicipioID, dbo.TbProvincia.ProvinciaID
FROM            dbo.TbAssembleia INNER JOIN
                         dbo.TbMunicipio ON dbo.TbAssembleia.MunicipioID = dbo.TbMunicipio.MunicipioID INNER JOIN
                         dbo.TbProvincia ON dbo.TbMunicipio.ProvinciaID = dbo.TbProvincia.ProvinciaID

GO
/****** Object:  View [dbo].[ViewGeral]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewGeral]
AS
SELECT        dbo.TbProvincia.ProvinciaID, dbo.TbProvincia.Designacao AS Provincia, dbo.TbGeral.QuantidadeAssembleias, dbo.TbGeral.QuantidadeEleitores, dbo.TbGeral.QuantidadeMesasVotos
FROM            dbo.TbGeral INNER JOIN
                         dbo.TbProvincia ON dbo.TbGeral.ProvinciaID = dbo.TbProvincia.ProvinciaID

GO
/****** Object:  View [dbo].[ViewMunicipio]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewMunicipio]
AS
SELECT        dbo.TbMunicipio.MunicipioID, dbo.TbMunicipio.Designacao AS Municipio, dbo.TbProvincia.ProvinciaID, dbo.TbProvincia.Designacao AS Provincia
FROM            dbo.TbMunicipio INNER JOIN
                         dbo.TbProvincia ON dbo.TbMunicipio.ProvinciaID = dbo.TbProvincia.ProvinciaID

GO
/****** Object:  View [dbo].[ViewResultados]    Script Date: 06/04/2022 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewResultados]
AS
SELECT        dbo.TbConcorrente.NomePartido, dbo.TbConcorrente.Sigla, dbo.TbResultadoConcorrente.NumeroVotos, dbo.TbResultadoConcorrente.ConcorrenteID, dbo.TbAssembleia.NumeroAssembleia, dbo.TbMunicipio.MunicipioID, 
                         dbo.TbProvincia.ProvinciaID, dbo.TbMunicipio.Designacao AS Municipio, dbo.TbProvincia.Designacao AS Provincia
FROM            dbo.TbResultadoConcorrente INNER JOIN
                         dbo.TbConcorrente ON dbo.TbResultadoConcorrente.ConcorrenteID = dbo.TbConcorrente.NumeroOrdem INNER JOIN
                         dbo.TbActa ON dbo.TbResultadoConcorrente.AssembleiaID = dbo.TbActa.AssembleiaID INNER JOIN
                         dbo.TbAssembleia INNER JOIN
                         dbo.TbMunicipio ON dbo.TbAssembleia.MunicipioID = dbo.TbMunicipio.MunicipioID INNER JOIN
                         dbo.TbProvincia ON dbo.TbMunicipio.ProvinciaID = dbo.TbProvincia.ProvinciaID ON dbo.TbActa.AssembleiaID = dbo.TbAssembleia.NumeroAssembleia

GO
/****** Object:  Index [IX_FK_TbActa_TbUsuario]    Script Date: 06/04/2022 20:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TbActa_TbUsuario] ON [dbo].[TbActa]
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TbAssembleia_TbMunicipio]    Script Date: 06/04/2022 20:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TbAssembleia_TbMunicipio] ON [dbo].[TbAssembleia]
(
	[MunicipioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TbMunicipio_TbProvincia]    Script Date: 06/04/2022 20:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TbMunicipio_TbProvincia] ON [dbo].[TbMunicipio]
(
	[ProvinciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TbResultadoConcorrente_TbActa]    Script Date: 06/04/2022 20:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TbResultadoConcorrente_TbActa] ON [dbo].[TbResultadoConcorrente]
(
	[AssembleiaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TbResultadoConcorrente_TbConcorrente]    Script Date: 06/04/2022 20:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TbResultadoConcorrente_TbConcorrente] ON [dbo].[TbResultadoConcorrente]
(
	[ConcorrenteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TbActa]  WITH CHECK ADD  CONSTRAINT [FK_TbActa_TbAssembleia] FOREIGN KEY([AssembleiaID])
REFERENCES [dbo].[TbAssembleia] ([NumeroAssembleia])
GO
ALTER TABLE [dbo].[TbActa] CHECK CONSTRAINT [FK_TbActa_TbAssembleia]
GO
ALTER TABLE [dbo].[TbActa]  WITH CHECK ADD  CONSTRAINT [FK_TbActa_TbUsuario] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[TbUsuario] ([UsuarioID])
GO
ALTER TABLE [dbo].[TbActa] CHECK CONSTRAINT [FK_TbActa_TbUsuario]
GO
ALTER TABLE [dbo].[TbAssembleia]  WITH CHECK ADD  CONSTRAINT [FK_TbAssembleia_TbMunicipio] FOREIGN KEY([MunicipioID])
REFERENCES [dbo].[TbMunicipio] ([MunicipioID])
GO
ALTER TABLE [dbo].[TbAssembleia] CHECK CONSTRAINT [FK_TbAssembleia_TbMunicipio]
GO
ALTER TABLE [dbo].[TbGeral]  WITH CHECK ADD  CONSTRAINT [FK_TbGeral_TbProvincia] FOREIGN KEY([ProvinciaID])
REFERENCES [dbo].[TbProvincia] ([ProvinciaID])
GO
ALTER TABLE [dbo].[TbGeral] CHECK CONSTRAINT [FK_TbGeral_TbProvincia]
GO
ALTER TABLE [dbo].[TbMunicipio]  WITH CHECK ADD  CONSTRAINT [FK_TbMunicipio_TbProvincia] FOREIGN KEY([ProvinciaID])
REFERENCES [dbo].[TbProvincia] ([ProvinciaID])
GO
ALTER TABLE [dbo].[TbMunicipio] CHECK CONSTRAINT [FK_TbMunicipio_TbProvincia]
GO
ALTER TABLE [dbo].[TbResultadoConcorrente]  WITH CHECK ADD  CONSTRAINT [FK_TbResultadoConcorrente_TbActa] FOREIGN KEY([AssembleiaID])
REFERENCES [dbo].[TbActa] ([AssembleiaID])
GO
ALTER TABLE [dbo].[TbResultadoConcorrente] CHECK CONSTRAINT [FK_TbResultadoConcorrente_TbActa]
GO
ALTER TABLE [dbo].[TbResultadoConcorrente]  WITH CHECK ADD  CONSTRAINT [FK_TbResultadoConcorrente_TbConcorrente] FOREIGN KEY([ConcorrenteID])
REFERENCES [dbo].[TbConcorrente] ([NumeroOrdem])
GO
ALTER TABLE [dbo].[TbResultadoConcorrente] CHECK CONSTRAINT [FK_TbResultadoConcorrente_TbConcorrente]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[21] 2[6] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TbActa"
            Begin Extent = 
               Top = 179
               Left = 632
               Bottom = 368
               Right = 815
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbAssembleia"
            Begin Extent = 
               Top = 38
               Left = 409
               Bottom = 151
               Right = 602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbMunicipio"
            Begin Extent = 
               Top = 173
               Left = 208
               Bottom = 286
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbProvincia"
            Begin Extent = 
               Top = 46
               Left = 8
               Bottom = 142
               Right = 178
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbUsuario"
            Begin Extent = 
               Top = 196
               Left = 403
               Bottom = 326
               Right = 573
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewActa'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewActa'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewActa'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[27] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TbAssembleia"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 127
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbMunicipio"
            Begin Extent = 
               Top = 22
               Left = 363
               Bottom = 135
               Right = 533
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbProvincia"
            Begin Extent = 
               Top = 39
               Left = 646
               Bottom = 135
               Right = 816
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAssembleia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAssembleia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TbGeral"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 254
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbProvincia"
            Begin Extent = 
               Top = 6
               Left = 292
               Bottom = 102
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGeral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGeral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TbMunicipio"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbProvincia"
            Begin Extent = 
               Top = 22
               Left = 364
               Bottom = 118
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMunicipio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMunicipio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[45] 2[7] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -177
      End
      Begin Tables = 
         Begin Table = "TbConcorrente"
            Begin Extent = 
               Top = 178
               Left = 12
               Bottom = 340
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbMunicipio"
            Begin Extent = 
               Top = 95
               Left = 857
               Bottom = 208
               Right = 1027
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbProvincia"
            Begin Extent = 
               Top = 111
               Left = 621
               Bottom = 207
               Right = 791
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbResultadoConcorrente"
            Begin Extent = 
               Top = 9
               Left = 220
               Bottom = 139
               Right = 390
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbAssembleia"
            Begin Extent = 
               Top = 265
               Left = 633
               Bottom = 378
               Right = 826
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TbActa"
            Begin Extent = 
               Top = 160
               Left = 421
               Bottom = 290
               Right = 604
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Widt' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewResultados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'h = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewResultados'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewResultados'
GO
USE [master]
GO
ALTER DATABASE [SICREE] SET  READ_WRITE 
GO
