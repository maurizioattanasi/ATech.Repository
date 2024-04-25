using System.Data;
using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.Repository
{
    public class PhysicalDimensionRepository : Repository<PhysicalDimension, long>, IPhysicalDimensionRepository
    {
        public PhysicalDimensionRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
