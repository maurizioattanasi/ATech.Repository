using ATech.Repository.EntityFrameworkCore;
using ATech.Repository.Test.Entities;
using ATech.Repository.Test.Repository;

namespace ATech.Repository.Test.EntityFrameworkCore.Repositories;

internal sealed class PhysicalDimensionRepository(AppDbContext context) : Repository<PhysicalDimension, long>(context), IPhysicalDimensionRepository
{
}

