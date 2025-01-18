using System;

using ATech.Repository.Test.Repositories;

namespace ATech.Repository.Test.Repository;

internal interface IIoTDataMartDbUnitOfWork : IDisposable
{
    IPhysicalDimensionRepository PhysicalDimensions { get; }

    ISensorRepository Sensors { get; }

    IMeasureRepository Measures { get; }
}
