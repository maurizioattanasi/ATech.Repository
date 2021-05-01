using System.Data;
using ATech.Repository.CrashTestDummy.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public class MeasureRepository : Repository<Measure>, IMeasureRepository
    {
        public MeasureRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
