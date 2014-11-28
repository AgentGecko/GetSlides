
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/28/2014 15:31:14
-- Generated from EDMX file: C:\Users\Danica\Documents\GitHub\GetSlides\GetSlides.DAL\GetSlidesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GetSlidesDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Users_AuthTokens]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthTokens] DROP CONSTRAINT [FK_Users_AuthTokens];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailTokens_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailTokens] DROP CONSTRAINT [FK_EmailTokens_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AuthTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthTokens];
GO
IF OBJECT_ID(N'[dbo].[EmailTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailTokens];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AuthTokens'
CREATE TABLE [dbo].[AuthTokens] (
    [ID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [Timespan] bigint  NOT NULL
);
GO

-- Creating table 'EmailTokens'
CREATE TABLE [dbo].[EmailTokens] (
    [ID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [StartDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'AuthTokens'
ALTER TABLE [dbo].[AuthTokens]
ADD CONSTRAINT [PK_AuthTokens]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'EmailTokens'
ALTER TABLE [dbo].[EmailTokens]
ADD CONSTRAINT [PK_EmailTokens]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'AuthTokens'
ALTER TABLE [dbo].[AuthTokens]
ADD CONSTRAINT [FK_Users_AuthTokens]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_AuthTokens'
CREATE INDEX [IX_FK_Users_AuthTokens]
ON [dbo].[AuthTokens]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'EmailTokens'
ALTER TABLE [dbo].[EmailTokens]
ADD CONSTRAINT [FK_EmailTokens_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailTokens_Users'
CREATE INDEX [IX_FK_EmailTokens_Users]
ON [dbo].[EmailTokens]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------