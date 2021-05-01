using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public class IoTDataMartDbUnitOfWork : IIoTDataMartDbUnitOfWork
    {
        private SqlTransaction transaction;

        public IoTDataMartDbUnitOfWork(SqlConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Connection.Open();
            transaction = Connection.BeginTransaction();
        }

        public SqlConnection Connection { get; }

        public int Complete()
        {
            transaction.Commit();
            
            return 0;
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            await transaction.CommitAsync(cancellationToken);

            return 0;
        }

        public void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}
