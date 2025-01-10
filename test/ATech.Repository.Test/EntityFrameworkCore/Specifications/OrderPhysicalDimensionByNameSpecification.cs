using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

public class OrderPhysicalDimensionByNameSpecification : Specification<PhysicalDimension>
{
    public OrderPhysicalDimensionByNameSpecification() => AddOrderBy(x => x.Name);
}

public class OrderPhysicalDimensionByNameDescendingSpecification : Specification<PhysicalDimension>
{
    public OrderPhysicalDimensionByNameDescendingSpecification() => AddOrderByDescending(x => x.Name);
}
