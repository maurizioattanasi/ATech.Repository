using System.Data;
using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.Repository
{
    public class SensorRepository : Repository<Sensor, long>, ISensorRepository
    {
        public SensorRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
