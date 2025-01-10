using System.Data;

using ATech.Repository.Dapper;
using ATech.Repository.Test.Entities;
using ATech.Repository.Test.Repositories;

namespace ATech.Repository.Test.Dapper.Repositories;

public class MeasureRepository(IDbConnection connection) : Repository<Measure, long>(connection), IMeasureRepository
{
}
