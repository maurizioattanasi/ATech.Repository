using System;

using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

internal sealed class GetPhysicalDimensionByNameSpecification(string name)
    : Specification<PhysicalDimension>(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
{
}
