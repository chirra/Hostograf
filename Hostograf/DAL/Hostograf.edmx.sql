
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/02/2015 16:55:53
-- Generated from EDMX file: C:\.vstudio\Hostograf\Hostograf\DAL\Hostograf.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Hostograf];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_HostTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test] DROP CONSTRAINT [FK_HostTest];
GO
IF OBJECT_ID(N'[dbo].[FK_TestICMP_inherits_Test]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test_TestICMP] DROP CONSTRAINT [FK_TestICMP_inherits_Test];
GO
IF OBJECT_ID(N'[dbo].[FK_TestTCP_inherits_Test]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test_TestTCP] DROP CONSTRAINT [FK_TestTCP_inherits_Test];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Host]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Host];
GO
IF OBJECT_ID(N'[dbo].[Test]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test];
GO
IF OBJECT_ID(N'[dbo].[Test_TestICMP]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test_TestICMP];
GO
IF OBJECT_ID(N'[dbo].[Test_TestTCP]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test_TestTCP];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Host'
CREATE TABLE [dbo].[Host] (
    [Id] uniqueidentifier  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Enabled] bit  NOT NULL
);
GO

-- Creating table 'Test'
CREATE TABLE [dbo].[Test] (
    [Id] uniqueidentifier  NOT NULL,
    [Enabled] bit  NOT NULL,
    [Host_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'Test_TestICMP'
CREATE TABLE [dbo].[Test_TestICMP] (
    [Address] nvarchar(max)  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Test_TestTCP'
CREATE TABLE [dbo].[Test_TestTCP] (
    [Address] nvarchar(max)  NOT NULL,
    [Port] nvarchar(max)  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Host'
ALTER TABLE [dbo].[Host]
ADD CONSTRAINT [PK_Host]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [PK_Test]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Test_TestICMP'
ALTER TABLE [dbo].[Test_TestICMP]
ADD CONSTRAINT [PK_Test_TestICMP]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Test_TestTCP'
ALTER TABLE [dbo].[Test_TestTCP]
ADD CONSTRAINT [PK_Test_TestTCP]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Host_Id] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [FK_HostTest]
    FOREIGN KEY ([Host_Id])
    REFERENCES [dbo].[Host]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostTest'
CREATE INDEX [IX_FK_HostTest]
ON [dbo].[Test]
    ([Host_Id]);
GO

-- Creating foreign key on [Id] in table 'Test_TestICMP'
ALTER TABLE [dbo].[Test_TestICMP]
ADD CONSTRAINT [FK_TestICMP_inherits_Test]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Test]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Test_TestTCP'
ALTER TABLE [dbo].[Test_TestTCP]
ADD CONSTRAINT [FK_TestTCP_inherits_Test]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Test]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------