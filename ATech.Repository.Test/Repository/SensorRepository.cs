using System.Data;
using ATech.Repository.Test.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.Test.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
