using System;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public interface IIoTDataMartDbUnitOfWork : IDisposable
    {
        IPhysicalDimensionRepository PhysicalDimensions { get; }

        ISensorRepository Sensors { get; }

        IMeasureRepository Measures { get; }
        
        int Complete();

        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
