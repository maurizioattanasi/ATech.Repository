using System;

using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

public class GetSensorByNameWithPhysicalDimensionSpecification : Specification<Sensor>
{
    public GetSensorByNameWithPhysicalDimensionSpecification(string name) 
        : base(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) => AddInclude(x => x.PhysicalDimension);
}