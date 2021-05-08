using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public class IoTDataMartDbUnitOfWork : IIoTDataMartDbUnitOfWork
    {
        private readonly SqlConnection connection;
        
        public IoTDataMartDbUnitOfWork(ref SqlConnection connection)
        {
            if(connection is null)
                throw new ArgumentNullException(nameof(connection));

            this.connection = new SqlConnection(connection.ConnectionString);
            this.connection.Open();

            // transaction = this.connection.BeginTransaction();

            PhysicalDimensions = new PhysicalDimensionRepository(this.connection);

            Sensors = new SensorRepository(this.connection);

            Measures = new MeasureRepository(this.connection);
        }

        public IPhysicalDimensionRepository PhysicalDimensions { get; }

        public ISensorRepository Sensors { get; }

        public IMeasureRepository Measures { get; }

        // public int Complete()
        // {
        //     // transaction.Commit();

        //     return 0;
        // }

        // public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        // {
        //     // await transaction.CommitAsync(cancellationToken);

        //     return 0;
        // }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
