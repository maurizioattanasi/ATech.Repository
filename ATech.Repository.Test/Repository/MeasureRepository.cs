using System.Data;
using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;

namespace ATech.Repository.Test.Repository
{
    public class MeasureRepository : Repository<Measure, long>, IMeasureRepository
    {
        public MeasureRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
