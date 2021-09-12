
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/13/2021 02:38:50
-- Generated from EDMX file: C:\Users\benva\source\repos\MoneyTrackerProject\MoneyTrackerProject\Models\MoneyTrackerDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MoneyTrackerDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DepartmentEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_DepartmentEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_DepartmentTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_EmployeeTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionTransactionMode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_TransactionTransactionMode];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[TransactionModes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionModes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [DepartmentId] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(max)  NOT NULL,
    [DepartmentFund] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [EmployeeName] nvarchar(max)  NOT NULL,
    [EmployeeRole] nvarchar(max)  NOT NULL,
    [DeptId] int  NOT NULL,
    [EmployeeEmail] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [TransactionId] int IDENTITY(1,1) NOT NULL,
    [ExpenseAmount] decimal(18,0)  NOT NULL,
    [TransactionDate] datetime  NOT NULL,
    [FKDeptId] int  NULL,
    [FKEmpId] int  NULL,
    [FKTransModeId] int  NOT NULL,
    [TransactionDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TransactionModes'
CREATE TABLE [dbo].[TransactionModes] (
    [ModeId] int IDENTITY(1,1) NOT NULL,
    [Mode] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DepartmentId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [TransactionId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([TransactionId] ASC);
GO

-- Creating primary key on [ModeId] in table 'TransactionModes'
ALTER TABLE [dbo].[TransactionModes]
ADD CONSTRAINT [PK_TransactionModes]
    PRIMARY KEY CLUSTERED ([ModeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DeptId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_DepartmentEmployee]
    FOREIGN KEY ([DeptId])
    REFERENCES [dbo].[Departments]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentEmployee'
CREATE INDEX [IX_FK_DepartmentEmployee]
ON [dbo].[Employees]
    ([DeptId]);
GO

-- Creating foreign key on [FKDeptId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_DepartmentTransaction]
    FOREIGN KEY ([FKDeptId])
    REFERENCES [dbo].[Departments]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentTransaction'
CREATE INDEX [IX_FK_DepartmentTransaction]
ON [dbo].[Transactions]
    ([FKDeptId]);
GO

-- Creating foreign key on [FKEmpId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_EmployeeTransaction]
    FOREIGN KEY ([FKEmpId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeTransaction'
CREATE INDEX [IX_FK_EmployeeTransaction]
ON [dbo].[Transactions]
    ([FKEmpId]);
GO

-- Creating foreign key on [FKTransModeId] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_TransactionTransactionMode]
    FOREIGN KEY ([FKTransModeId])
    REFERENCES [dbo].[TransactionModes]
        ([ModeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionTransactionMode'
CREATE INDEX [IX_FK_TransactionTransactionMode]
ON [dbo].[Transactions]
    ([FKTransModeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------