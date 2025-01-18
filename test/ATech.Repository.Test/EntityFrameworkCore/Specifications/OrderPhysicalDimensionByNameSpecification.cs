using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

internal sealed class OrderPhysicalDimensionByNameSpecification : Specification<PhysicalDimension>
{
    public OrderPhysicalDimensionByNameSpecification() => AddOrderBy(x => x.Name);
}

internal sealed class OrderPhysicalDimensionByNameDescendingSpecification : Specification<PhysicalDimension>
{
    public OrderPhysicalDimensionByNameDescendingSpecification() => AddOrderByDescending(x => x.Name);
}
