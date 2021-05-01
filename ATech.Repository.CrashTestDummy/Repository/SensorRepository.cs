using System.Data;
using ATech.Repository.CrashTestDummy.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.CrashTestDummy.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
