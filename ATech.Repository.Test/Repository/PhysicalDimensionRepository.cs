using System.Data;
using ATech.Repository.Test.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.Test.Repository
{
    public class PhysicalDimensionRepository : Repository<PhysicalDimension>, IPhysicalDimensionRepository
    {
        public PhysicalDimensionRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
