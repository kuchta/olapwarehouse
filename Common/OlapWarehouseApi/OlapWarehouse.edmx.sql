
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/30/2013 17:21:37
-- Generated from EDMX file: C:\Users\mkuchta\Projects\Databases\Common\OlapWarehouseApi\OlapWarehouse.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OlapWarehouse];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DimensionElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_DimensionElement];
GO
IF OBJECT_ID(N'[dbo].[FK_ElementAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [FK_ElementAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_DatabaseDimension]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dimensions] DROP CONSTRAINT [FK_DatabaseDimension];
GO
IF OBJECT_ID(N'[dbo].[FK_CubeDimension]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dimensions] DROP CONSTRAINT [FK_CubeDimension];
GO
IF OBJECT_ID(N'[dbo].[FK_DatabaseCube]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cubes] DROP CONSTRAINT [FK_DatabaseCube];
GO
IF OBJECT_ID(N'[dbo].[FK_ElementElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_ElementElement];
GO
IF OBJECT_ID(N'[dbo].[FK_DimensionDimension]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dimensions] DROP CONSTRAINT [FK_DimensionDimension];
GO
IF OBJECT_ID(N'[dbo].[FK_CubeFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facts] DROP CONSTRAINT [FK_CubeFact];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Elements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Elements];
GO
IF OBJECT_ID(N'[dbo].[Dimensions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dimensions];
GO
IF OBJECT_ID(N'[dbo].[Databases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Databases];
GO
IF OBJECT_ID(N'[dbo].[Cubes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cubes];
GO
IF OBJECT_ID(N'[dbo].[Attributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attributes];
GO
IF OBJECT_ID(N'[dbo].[Facts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Facts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Elements'
CREATE TABLE [dbo].[Elements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Caption] nvarchar(max)  NULL,
    [Weight] real  NULL,
    [Order] smallint  NULL,
    [DimensionId] int  NOT NULL,
    [ElementId] int  NULL
);
GO

-- Creating table 'Dimensions'
CREATE TABLE [dbo].[Dimensions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [DatabaseId] int  NULL,
    [CubeId] int  NULL,
    [DimensionId] int  NULL
);
GO

-- Creating table 'Servers'
CREATE TABLE [dbo].[Servers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'Cubes'
CREATE TABLE [dbo].[Cubes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [DatabaseId] int  NOT NULL
);
GO

-- Creating table 'Attributes'
CREATE TABLE [dbo].[Attributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ElementId] int  NOT NULL
);
GO

-- Creating table 'Facts'
CREATE TABLE [dbo].[Facts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CubeId] int  NOT NULL,
    [ElementId1] nvarchar(50)  NOT NULL,
    [ElementId2] nvarchar(50)  NOT NULL,
    [ElementId3] nvarchar(50)  NULL,
    [ElementId4] nvarchar(50)  NULL,
    [ElementId5] nvarchar(50)  NULL,
    [Value] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [PK_Elements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dimensions'
ALTER TABLE [dbo].[Dimensions]
ADD CONSTRAINT [PK_Dimensions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Servers'
ALTER TABLE [dbo].[Servers]
ADD CONSTRAINT [PK_Servers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cubes'
ALTER TABLE [dbo].[Cubes]
ADD CONSTRAINT [PK_Cubes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attributes'
ALTER TABLE [dbo].[Attributes]
ADD CONSTRAINT [PK_Attributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Facts'
ALTER TABLE [dbo].[Facts]
ADD CONSTRAINT [PK_Facts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DimensionId] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [FK_DimensionElement]
    FOREIGN KEY ([DimensionId])
    REFERENCES [dbo].[Dimensions]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DimensionElement'
CREATE INDEX [IX_FK_DimensionElement]
ON [dbo].[Elements]
    ([DimensionId]);
GO

-- Creating foreign key on [ElementId] in table 'Attributes'
ALTER TABLE [dbo].[Attributes]
ADD CONSTRAINT [FK_ElementAttribute]
    FOREIGN KEY ([ElementId])
    REFERENCES [dbo].[Elements]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ElementAttribute'
CREATE INDEX [IX_FK_ElementAttribute]
ON [dbo].[Attributes]
    ([ElementId]);
GO

-- Creating foreign key on [DatabaseId] in table 'Dimensions'
ALTER TABLE [dbo].[Dimensions]
ADD CONSTRAINT [FK_DatabaseDimension]
    FOREIGN KEY ([DatabaseId])
    REFERENCES [dbo].[Servers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DatabaseDimension'
CREATE INDEX [IX_FK_DatabaseDimension]
ON [dbo].[Dimensions]
    ([DatabaseId]);
GO

-- Creating foreign key on [CubeId] in table 'Dimensions'
ALTER TABLE [dbo].[Dimensions]
ADD CONSTRAINT [FK_CubeDimension]
    FOREIGN KEY ([CubeId])
    REFERENCES [dbo].[Cubes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CubeDimension'
CREATE INDEX [IX_FK_CubeDimension]
ON [dbo].[Dimensions]
    ([CubeId]);
GO

-- Creating foreign key on [DatabaseId] in table 'Cubes'
ALTER TABLE [dbo].[Cubes]
ADD CONSTRAINT [FK_DatabaseCube]
    FOREIGN KEY ([DatabaseId])
    REFERENCES [dbo].[Servers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DatabaseCube'
CREATE INDEX [IX_FK_DatabaseCube]
ON [dbo].[Cubes]
    ([DatabaseId]);
GO

-- Creating foreign key on [ElementId] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [FK_ElementElement]
    FOREIGN KEY ([ElementId])
    REFERENCES [dbo].[Elements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ElementElement'
CREATE INDEX [IX_FK_ElementElement]
ON [dbo].[Elements]
    ([ElementId]);
GO

-- Creating foreign key on [DimensionId] in table 'Dimensions'
ALTER TABLE [dbo].[Dimensions]
ADD CONSTRAINT [FK_DimensionDimension]
    FOREIGN KEY ([DimensionId])
    REFERENCES [dbo].[Dimensions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DimensionDimension'
CREATE INDEX [IX_FK_DimensionDimension]
ON [dbo].[Dimensions]
    ([DimensionId]);
GO

-- Creating foreign key on [CubeId] in table 'Facts'
ALTER TABLE [dbo].[Facts]
ADD CONSTRAINT [FK_CubeFact]
    FOREIGN KEY ([CubeId])
    REFERENCES [dbo].[Cubes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CubeFact'
CREATE INDEX [IX_FK_CubeFact]
ON [dbo].[Facts]
    ([CubeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------