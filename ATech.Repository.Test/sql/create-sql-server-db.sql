USE [master]
GO

-- Close All Connections
IF EXISTS (SELECT name
FROM sys.databases
WHERE name = N'ATech.IoTDataMart')
ALTER DATABASE [ATech.IoTDataMart] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

-- Drop database if exists
IF EXISTS (SELECT name
FROM sys.databases
WHERE name = N'ATech.IoTDataMart')
    DROP DATABASE [ATech.IoTDataMart]
    GO

-- Create new database
CREATE DATABASE [ATech.IoTDataMart]
GO

ALTER DATABASE [ATech.IoTDataMart] SET RECOVERY SIMPLE
GO

USE [ATech.IoTDataMart]
GO

-- Physical Dimension Table
CREATE TABLE [dbo].[PhysicalDimension]
(
    [Id] SMALLINT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Scale] NVARCHAR(32) NULL,
    [Created] DATETIME2 NOT NULL,
    [CreatedBy] NVARCHAR(255) NULL,
    CONSTRAINT PK_PhysicalDimension_Id PRIMARY KEY (ID) WITH (FILLFACTOR = 100),
    CONSTRAINT UX_PhysicalDimension_Name UNIQUE(Name)
)
GO

-- Sensor Table
CREATE TABLE [dbo].[Sensor]
(
    [Id] SMALLINT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Make] NVARCHAR(255) NULL,
    [Model] NVARCHAR(255) NULL,
    [SerialNumber] NVARCHAR(255) NULL,
    [PhysicalDimensionId] SMALLINT NOT NULL,
    [Created] DATETIME2 NOT NULL,
    [CreatedBy] NVARCHAR(255) NULL,
    CONSTRAINT PK_Sensor_Id PRIMARY KEY CLUSTERED (ID) WITH (FILLFACTOR = 100),
    CONSTRAINT UX_Sensor_Name UNIQUE(Name),
    CONSTRAINT FK_Sensor_PhysicalDimension_PhysicalDimensionId FOREIGN KEY ([PhysicalDimensionId]) REFERENCES [dbo].[PhysicalDimension]([Id]) ON DELETE CASCADE
)
GO

CREATE NONCLUSTERED INDEX [IX_Sensor_PhysicalDimensionId] ON [dbo].[Sensor]([PhysicalDimensionId] ASC)
GO
CREATE INDEX [IX_Sensor_Name] ON [dbo].[Sensor]([Name])
GO

-- Measure Table
CREATE TABLE [dbo].[Measure]
(
    [Id] BIGINT IDENTITY(1, 1) NOT NULL,
    [SensorId] SMALLINT NOT NULL,
    [Value] FLOAT NOT NULL,
    [Created] DATETIME2 NOT NULL,
    CONSTRAINT PK_Measure_Id PRIMARY KEY CLUSTERED (ID) WITH (FILLFACTOR = 100),
    CONSTRAINT FK_Measure_Sensor_SensorId FOREIGN KEY ([SensorID]) REFERENCES [dbo].[Sensor] ON DELETE CASCADE
)
GO
CREATE NONCLUSTERED INDEX [IX_Measure_SensorId] ON [dbo].[Measure]([SensorId] ASC)
GO
CREATE INDEX [IX_Measure_Created] ON [dbo].[Measure]([Created] ASC)
GO