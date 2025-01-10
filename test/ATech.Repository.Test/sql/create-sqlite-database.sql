DROP TABLE IF EXISTS [PhysicalDimension];

CREATE TABLE [PhysicalDimension]
(
    [Id] INTEGER PRIMARY KEY,
    [Name] TEXT UNIQUE NOT NULL,
    [Scale] TEXT NULL,
    [Created] DATETIME2 NOT NULL,
    [CreatedBy] TEXT NULL
);

DROP TABLE IF EXISTS [Sensor];
CREATE TABLE [Sensor]
(
    [Id] INTEGER PRIMARY KEY,
    [Name] TEXT UNIQUE NOT NULL,
    [Make] TEXT NULL,
    [Model] TEXT NULL,
    [SerialNumber] TEXT NULL,
    [PhysicalDimensionId] INTEGER NOT NULL,
    [Created] DATETIME2 NOT NULL,
    [CreatedBy] TEXT NULL,
    FOREIGN KEY([PhysicalDimensionId]) REFERENCES [PhysicalDimension]([Id]) ON DELETE CASCADE
);

DROP TABLE IF EXISTS [Measure];
CREATE TABLE [Measure]
(
    [Id] INTEGER PRIMARY KEY,
    [SensorId] INTEGER NOT NULL,
    [Value] FLOAT NOT NULL,
    [Created] DATETIME2 NOT NULL,
    FOREIGN KEY([SensorId]) REFERENCES [Sensor]([Id]) ON DELETE CASCADE
);