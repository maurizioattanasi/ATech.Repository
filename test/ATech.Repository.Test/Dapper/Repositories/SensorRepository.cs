using System.Data;

using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;
using ATech.Repository.Test.Repository;

namespace ATech.Repository.Test.Dapper.Repositories;

public class SensorRepository : Repository<Sensor, long>, ISensorRepository
{
    public SensorRepository(IDbConnection connection) : base(connection)
    {
    }
}
