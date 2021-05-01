using System.Data;
using ATech.Repository.CrashTestDummy.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public class PhysicalDimensionRepository : Repository<PhysicalDimension>, IPhysicalDimensionRepository
    {
        public PhysicalDimensionRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
