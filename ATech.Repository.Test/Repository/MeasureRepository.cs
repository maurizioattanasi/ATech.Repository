using System.Data;
using ATech.Repository.Test.Entities;
using ATech.Repository.Dapper;

namespace ATech.Repository.Test.Repository
{
    public class MeasureRepository : Repository<Measure>, IMeasureRepository
    {
        public MeasureRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
