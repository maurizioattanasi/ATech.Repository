using System;

using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

public class GetPhysicalDimensionByNameSpecification(string name)
    : Specification<PhysicalDimension>(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
{
}
