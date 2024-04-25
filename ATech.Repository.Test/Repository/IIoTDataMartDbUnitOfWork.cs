using System;

namespace ATech.Repository.Test.Repository
{
    public interface IIoTDataMartDbUnitOfWork : IDisposable
    {
        IPhysicalDimensionRepository PhysicalDimensions { get; }

        ISensorRepository Sensors { get; }

        IMeasureRepository Measures { get; }
    }
}
