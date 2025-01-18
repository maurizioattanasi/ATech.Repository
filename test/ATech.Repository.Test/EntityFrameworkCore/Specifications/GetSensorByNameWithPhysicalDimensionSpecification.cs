using System;

using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

internal sealed class GetSensorByNameWithPhysicalDimensionSpecification : Specification<Sensor>
{
    public GetSensorByNameWithPhysicalDimensionSpecification(string name) 
        : base(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) => AddInclude(x => x.PhysicalDimension);
}