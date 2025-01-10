using System.Data;

using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;
using ATech.Repository.Test.Repository;

namespace ATech.Repository.Test.Dapper.Repositories;

public class PhysicalDimensionRepository(IDbConnection connection) : Repository<PhysicalDimension, long>(connection), IPhysicalDimensionRepository
{
}
