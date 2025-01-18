using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.EntityFrameworkCore.Specifications;

internal sealed class PaginationSpecification(int? pageIndex = null, int? pageSize = null) : Specification<PhysicalDimension>(skip: ((pageIndex ?? 0) - 1) * pageSize ?? 0, take: pageSize)
{
}
