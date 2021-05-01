using System;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public interface IIoTDataMartDbUnitOfWork : IDisposable
    {
        int Complete();

        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
