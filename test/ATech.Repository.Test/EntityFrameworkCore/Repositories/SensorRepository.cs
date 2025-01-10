using ATech.Repository.EntityFrameworkCore;
using ATech.Repository.Test.Entities;
using ATech.Repository.Test.Repository;

namespace ATech.Repository.Test.EntityFrameworkCore.Repositories;

public class SensorRepository(AppDbContext context) : Repository<Sensor, long>(context), ISensorRepository
{
}

