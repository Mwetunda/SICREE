
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/03/2021 09:38:49
-- Generated from EDMX file: D:\PROJECTOS\SICREE Actual PCP\ACESSOABASEDEDADOS\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SICREE];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TbActa_TbAssembleia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbActa] DROP CONSTRAINT [FK_TbActa_TbAssembleia];
GO
IF OBJECT_ID(N'[dbo].[FK_TbActa_TbUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbActa] DROP CONSTRAINT [FK_TbActa_TbUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_TbResultadoConcorrente_TbActa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbResultadoConcorrente] DROP CONSTRAINT [FK_TbResultadoConcorrente_TbActa];
GO
IF OBJECT_ID(N'[dbo].[FK_TbAssembleia_TbMunicipio]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbAssembleia] DROP CONSTRAINT [FK_TbAssembleia_TbMunicipio];
GO
IF OBJECT_ID(N'[dbo].[FK_TbResultadoConcorrente_TbConcorrente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbResultadoConcorrente] DROP CONSTRAINT [FK_TbResultadoConcorrente_TbConcorrente];
GO
IF OBJECT_ID(N'[dbo].[FK_TbGeral_TbProvincia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbGeral] DROP CONSTRAINT [FK_TbGeral_TbProvincia];
GO
IF OBJECT_ID(N'[dbo].[FK_TbMunicipio_TbProvincia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TbMunicipio] DROP CONSTRAINT [FK_TbMunicipio_TbProvincia];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TbActa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbActa];
GO
IF OBJECT_ID(N'[dbo].[TbAssembleia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbAssembleia];
GO
IF OBJECT_ID(N'[dbo].[TbConcorrente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbConcorrente];
GO
IF OBJECT_ID(N'[dbo].[TbGeral]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbGeral];
GO
IF OBJECT_ID(N'[dbo].[TbMunicipio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbMunicipio];
GO
IF OBJECT_ID(N'[dbo].[TbProvincia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbProvincia];
GO
IF OBJECT_ID(N'[dbo].[TbResultadoConcorrente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbResultadoConcorrente];
GO
IF OBJECT_ID(N'[dbo].[TbUsuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TbUsuario];
GO
IF OBJECT_ID(N'[dbo].[ViewActa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewActa];
GO
IF OBJECT_ID(N'[dbo].[ViewAssembleia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewAssembleia];
GO
IF OBJECT_ID(N'[dbo].[ViewGeral]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewGeral];
GO
IF OBJECT_ID(N'[dbo].[ViewMunicipio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewMunicipio];
GO
IF OBJECT_ID(N'[dbo].[ViewResultados]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ViewResultados];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TbActa'
CREATE TABLE [dbo].[TbActa] (
    [AssembleiaID] int  NOT NULL,
    [UsuarioID] int  NULL,
    [QtdMesa] int  NULL,
    [VotosBrancos] int  NULL,
    [VotosNulos] int  NULL,
    [VotosReclamados] int  NULL,
    [VotosValidos] int  NULL
);
GO

-- Creating table 'TbAssembleia'
CREATE TABLE [dbo].[TbAssembleia] (
    [NumeroAssembleia] int  NOT NULL,
    [MunicipioID] int  NULL,
    [Endereco] varchar(70)  NULL
);
GO

-- Creating table 'TbConcorrente'
CREATE TABLE [dbo].[TbConcorrente] (
    [NumeroOrdem] int  NOT NULL,
    [NomePartido] varchar(100)  NOT NULL,
    [Sigla] varchar(10)  NOT NULL,
    [NomePresidente] varchar(50)  NOT NULL,
    [BandeiraPartido] varbinary(max)  NOT NULL,
    [FotoPresidente] varbinary(max)  NOT NULL
);
GO

-- Creating table 'TbGeral'
CREATE TABLE [dbo].[TbGeral] (
    [ProvinciaID] int  NOT NULL,
    [QuantidadeAssembleias] int  NULL,
    [QuantidadeEleitores] int  NULL,
    [QuantidadeMesasVotos] int  NULL
);
GO

-- Creating table 'TbMunicipio'
CREATE TABLE [dbo].[TbMunicipio] (
    [MunicipioID] int IDENTITY(1,1) NOT NULL,
    [ProvinciaID] int  NULL,
    [Designacao] varchar(20)  NULL
);
GO

-- Creating table 'TbProvincia'
CREATE TABLE [dbo].[TbProvincia] (
    [ProvinciaID] int IDENTITY(1,1) NOT NULL,
    [Designacao] varchar(15)  NULL
);
GO

-- Creating table 'TbResultadoConcorrente'
CREATE TABLE [dbo].[TbResultadoConcorrente] (
    [ResultadoID] int IDENTITY(1,1) NOT NULL,
    [ConcorrenteID] int  NOT NULL,
    [NumeroVotos] int  NOT NULL,
    [AssembleiaID] int  NULL
);
GO

-- Creating table 'TbUsuario'
CREATE TABLE [dbo].[TbUsuario] (
    [UsuarioID] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(50)  NULL,
    [Login] varchar(20)  NULL,
    [Senha] varchar(10)  NULL,
    [Telefone] varchar(10)  NULL,
    [Estado] bit  NULL,
    [Previlegio] varchar(15)  NULL
);
GO

-- Creating table 'ViewActa'
CREATE TABLE [dbo].[ViewActa] (
    [NumeroAssembleia] int  NOT NULL,
    [Endereco] varchar(70)  NULL,
    [Usuario] varchar(50)  NULL,
    [UsuarioID] int  NOT NULL,
    [MunicipioID] int  NOT NULL,
    [Municipio] varchar(20)  NULL,
    [ProvinciaID] int  NOT NULL,
    [Provincia] varchar(15)  NULL,
    [QtdMesa] int  NULL,
    [VotosBrancos] int  NULL,
    [VotosNulos] int  NULL,
    [VotosReclamados] int  NULL,
    [VotosValidos] int  NULL
);
GO

-- Creating table 'ViewAssembleia'
CREATE TABLE [dbo].[ViewAssembleia] (
    [NumeroAssembleia] int  NOT NULL,
    [Endereco] varchar(70)  NULL,
    [Municipio] varchar(20)  NULL,
    [Provincia] varchar(15)  NULL,
    [MunicipioID] int  NOT NULL,
    [ProvinciaID] int  NOT NULL
);
GO

-- Creating table 'ViewGeral'
CREATE TABLE [dbo].[ViewGeral] (
    [ProvinciaID] int  NOT NULL,
    [Provincia] varchar(15)  NULL,
    [QuantidadeEleitores] int  NULL,
    [QuantidadeAssembleias] int  NULL,
    [QuantidadeMesasVotos] int  NULL
);
GO

-- Creating table 'ViewMunicipio'
CREATE TABLE [dbo].[ViewMunicipio] (
    [MunicipioID] int  NOT NULL,
    [Municipio] varchar(20)  NULL,
    [ProvinciaID] int  NOT NULL,
    [Provincia] varchar(15)  NULL
);
GO

-- Creating table 'ViewResultados'
CREATE TABLE [dbo].[ViewResultados] (
    [NomePartido] varchar(100)  NOT NULL,
    [Sigla] varchar(10)  NOT NULL,
    [NumeroVotos] int  NOT NULL,
    [ConcorrenteID] int  NOT NULL,
    [NumeroAssembleia] int  NOT NULL,
    [MunicipioID] int  NOT NULL,
    [ProvinciaID] int  NOT NULL,
    [Municipio] varchar(20)  NULL,
    [Provincia] varchar(15)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [AssembleiaID] in table 'TbActa'
ALTER TABLE [dbo].[TbActa]
ADD CONSTRAINT [PK_TbActa]
    PRIMARY KEY CLUSTERED ([AssembleiaID] ASC);
GO

-- Creating primary key on [NumeroAssembleia] in table 'TbAssembleia'
ALTER TABLE [dbo].[TbAssembleia]
ADD CONSTRAINT [PK_TbAssembleia]
    PRIMARY KEY CLUSTERED ([NumeroAssembleia] ASC);
GO

-- Creating primary key on [NumeroOrdem] in table 'TbConcorrente'
ALTER TABLE [dbo].[TbConcorrente]
ADD CONSTRAINT [PK_TbConcorrente]
    PRIMARY KEY CLUSTERED ([NumeroOrdem] ASC);
GO

-- Creating primary key on [ProvinciaID] in table 'TbGeral'
ALTER TABLE [dbo].[TbGeral]
ADD CONSTRAINT [PK_TbGeral]
    PRIMARY KEY CLUSTERED ([ProvinciaID] ASC);
GO

-- Creating primary key on [MunicipioID] in table 'TbMunicipio'
ALTER TABLE [dbo].[TbMunicipio]
ADD CONSTRAINT [PK_TbMunicipio]
    PRIMARY KEY CLUSTERED ([MunicipioID] ASC);
GO

-- Creating primary key on [ProvinciaID] in table 'TbProvincia'
ALTER TABLE [dbo].[TbProvincia]
ADD CONSTRAINT [PK_TbProvincia]
    PRIMARY KEY CLUSTERED ([ProvinciaID] ASC);
GO

-- Creating primary key on [ResultadoID] in table 'TbResultadoConcorrente'
ALTER TABLE [dbo].[TbResultadoConcorrente]
ADD CONSTRAINT [PK_TbResultadoConcorrente]
    PRIMARY KEY CLUSTERED ([ResultadoID] ASC);
GO

-- Creating primary key on [UsuarioID] in table 'TbUsuario'
ALTER TABLE [dbo].[TbUsuario]
ADD CONSTRAINT [PK_TbUsuario]
    PRIMARY KEY CLUSTERED ([UsuarioID] ASC);
GO

-- Creating primary key on [NumeroAssembleia], [UsuarioID], [MunicipioID], [ProvinciaID] in table 'ViewActa'
ALTER TABLE [dbo].[ViewActa]
ADD CONSTRAINT [PK_ViewActa]
    PRIMARY KEY CLUSTERED ([NumeroAssembleia], [UsuarioID], [MunicipioID], [ProvinciaID] ASC);
GO

-- Creating primary key on [NumeroAssembleia], [MunicipioID], [ProvinciaID] in table 'ViewAssembleia'
ALTER TABLE [dbo].[ViewAssembleia]
ADD CONSTRAINT [PK_ViewAssembleia]
    PRIMARY KEY CLUSTERED ([NumeroAssembleia], [MunicipioID], [ProvinciaID] ASC);
GO

-- Creating primary key on [ProvinciaID] in table 'ViewGeral'
ALTER TABLE [dbo].[ViewGeral]
ADD CONSTRAINT [PK_ViewGeral]
    PRIMARY KEY CLUSTERED ([ProvinciaID] ASC);
GO

-- Creating primary key on [MunicipioID], [ProvinciaID] in table 'ViewMunicipio'
ALTER TABLE [dbo].[ViewMunicipio]
ADD CONSTRAINT [PK_ViewMunicipio]
    PRIMARY KEY CLUSTERED ([MunicipioID], [ProvinciaID] ASC);
GO

-- Creating primary key on [NomePartido], [Sigla], [NumeroVotos], [ConcorrenteID], [NumeroAssembleia], [MunicipioID], [ProvinciaID] in table 'ViewResultados'
ALTER TABLE [dbo].[ViewResultados]
ADD CONSTRAINT [PK_ViewResultados]
    PRIMARY KEY CLUSTERED ([NomePartido], [Sigla], [NumeroVotos], [ConcorrenteID], [NumeroAssembleia], [MunicipioID], [ProvinciaID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AssembleiaID] in table 'TbActa'
ALTER TABLE [dbo].[TbActa]
ADD CONSTRAINT [FK_TbActa_TbAssembleia]
    FOREIGN KEY ([AssembleiaID])
    REFERENCES [dbo].[TbAssembleia]
        ([NumeroAssembleia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UsuarioID] in table 'TbActa'
ALTER TABLE [dbo].[TbActa]
ADD CONSTRAINT [FK_TbActa_TbUsuario]
    FOREIGN KEY ([UsuarioID])
    REFERENCES [dbo].[TbUsuario]
        ([UsuarioID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TbActa_TbUsuario'
CREATE INDEX [IX_FK_TbActa_TbUsuario]
ON [dbo].[TbActa]
    ([UsuarioID]);
GO

-- Creating foreign key on [AssembleiaID] in table 'TbResultadoConcorrente'
ALTER TABLE [dbo].[TbResultadoConcorrente]
ADD CONSTRAINT [FK_TbResultadoConcorrente_TbActa]
    FOREIGN KEY ([AssembleiaID])
    REFERENCES [dbo].[TbActa]
        ([AssembleiaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TbResultadoConcorrente_TbActa'
CREATE INDEX [IX_FK_TbResultadoConcorrente_TbActa]
ON [dbo].[TbResultadoConcorrente]
    ([AssembleiaID]);
GO

-- Creating foreign key on [MunicipioID] in table 'TbAssembleia'
ALTER TABLE [dbo].[TbAssembleia]
ADD CONSTRAINT [FK_TbAssembleia_TbMunicipio]
    FOREIGN KEY ([MunicipioID])
    REFERENCES [dbo].[TbMunicipio]
        ([MunicipioID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TbAssembleia_TbMunicipio'
CREATE INDEX [IX_FK_TbAssembleia_TbMunicipio]
ON [dbo].[TbAssembleia]
    ([MunicipioID]);
GO

-- Creating foreign key on [ConcorrenteID] in table 'TbResultadoConcorrente'
ALTER TABLE [dbo].[TbResultadoConcorrente]
ADD CONSTRAINT [FK_TbResultadoConcorrente_TbConcorrente]
    FOREIGN KEY ([ConcorrenteID])
    REFERENCES [dbo].[TbConcorrente]
        ([NumeroOrdem])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TbResultadoConcorrente_TbConcorrente'
CREATE INDEX [IX_FK_TbResultadoConcorrente_TbConcorrente]
ON [dbo].[TbResultadoConcorrente]
    ([ConcorrenteID]);
GO

-- Creating foreign key on [ProvinciaID] in table 'TbGeral'
ALTER TABLE [dbo].[TbGeral]
ADD CONSTRAINT [FK_TbGeral_TbProvincia]
    FOREIGN KEY ([ProvinciaID])
    REFERENCES [dbo].[TbProvincia]
        ([ProvinciaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProvinciaID] in table 'TbMunicipio'
ALTER TABLE [dbo].[TbMunicipio]
ADD CONSTRAINT [FK_TbMunicipio_TbProvincia]
    FOREIGN KEY ([ProvinciaID])
    REFERENCES [dbo].[TbProvincia]
        ([ProvinciaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TbMunicipio_TbProvincia'
CREATE INDEX [IX_FK_TbMunicipio_TbProvincia]
ON [dbo].[TbMunicipio]
    ([ProvinciaID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------