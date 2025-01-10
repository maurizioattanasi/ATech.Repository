using System;
using System.Data;
using System.Data.SqlClient;

using ATech.Repository.Test.Repository;
using ATech.Repository.Test.Repositories;

using Microsoft.Data.Sqlite;

namespace ATech.Repository.Test.Dapper.Repositories;

public class IoTDataMartDbUnitOfWork : IIoTDataMartDbUnitOfWork
{
    private readonly IDbConnection connection;

    public IoTDataMartDbUnitOfWork(ref IDbConnection connection)
    {
        if (connection is null)
            throw new ArgumentNullException(nameof(connection));

        if (connection is SqlConnection)
            this.connection = new SqlConnection(connection.ConnectionString);
        else
            this.connection = new SqliteConnection(connection.ConnectionString);

        this.connection.Open();

        // transaction = this.connection.BeginTransaction();

        PhysicalDimensions = new PhysicalDimensionRepository(this.connection);

        Sensors = new SensorRepository(this.connection);

        Measures = new MeasureRepository(this.connection);
    }

    public IPhysicalDimensionRepository PhysicalDimensions { get; }

    public ISensorRepository Sensors { get; }

    public IMeasureRepository Measures { get; }

    public void Dispose()
    {
        connection.Dispose();
    }
}
