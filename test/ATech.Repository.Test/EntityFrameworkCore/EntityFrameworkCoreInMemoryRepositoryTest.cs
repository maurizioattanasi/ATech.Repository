using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

using ATech.Repository.Test.Entities;

using ATech.Repository.Test.EntityFrameworkCore.Repositories;
using ATech.Repository.Test.EntityFrameworkCore.Specifications;

using Microsoft.EntityFrameworkCore;


using Xunit;


namespace ATech.Repository.Test.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class EntityFrameworkCoreInMemoryRepositoryTest
{
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

    [Fact]
    public async Task CRUDTest()
    {
        // Arrange
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("MyInMemoryDatabase")
            .Options;

        using var context = new AppDbContext(options);

        var physicalDimensionRepository = new PhysicalDimensionRepository(context);

        int physicalDimensionsCount = await physicalDimensionRepository.CountAsync(_cancellationToken);

        Assert.Equal(0, physicalDimensionsCount);

        // Adds a new row
        physicalDimensionRepository
            .Add(new PhysicalDimension
            {
                Name = "Temperature",
                Scale = "%",
                Created = DateTime.UtcNow,
                CreatedBy = "atech"
            });

        await physicalDimensionRepository.SaveChangesAsync(_cancellationToken);

        physicalDimensionsCount = await physicalDimensionRepository.CountAsync(_cancellationToken);

        Assert.Equal(1, physicalDimensionsCount);

        PhysicalDimension? humidityPhysicalDimension = await physicalDimensionRepository
            .FirstOrDefaultAsync(new GetPhysicalDimensionByNameSpecification("Temperature"), _cancellationToken);

        Assert.NotNull(humidityPhysicalDimension);

        physicalDimensionRepository
            .Add(new PhysicalDimension
            {
                Name = "Humidity",
                Scale = "°",
                Created = DateTime.UtcNow,
                CreatedBy = "atech"
            });

        await physicalDimensionRepository.SaveChangesAsync(_cancellationToken);

        Assert.True(await physicalDimensionRepository.AnyAsync(_cancellationToken));

        System.Collections.Generic.List<PhysicalDimension> allPhysicalDimensions = await physicalDimensionRepository.ListAsync(_cancellationToken);

        Assert.Equal(2, allPhysicalDimensions.Count);

        Assert.True(await physicalDimensionRepository.AnyAsync(new GetPhysicalDimensionByNameSpecification("Temperature"), _cancellationToken));

        allPhysicalDimensions = await physicalDimensionRepository.ListAsync(new GetPhysicalDimensionByNameSpecification("Temperature"), _cancellationToken);

        Assert.Single(allPhysicalDimensions);

        System.Collections.Generic.List<PhysicalDimension> orderedByNamePhysicalDimensions = await physicalDimensionRepository.ListAsync(new OrderPhysicalDimensionByNameSpecification(), _cancellationToken);

        Assert.Equal(2, await physicalDimensionRepository.CountAsync(new OrderPhysicalDimensionByNameSpecification(), _cancellationToken));

        Assert.Equal("Humidity", orderedByNamePhysicalDimensions[0].Name);

        System.Collections.Generic.List<PhysicalDimension> orderedByNameDescPhysicalDimensions = await physicalDimensionRepository.ListAsync(new OrderPhysicalDimensionByNameDescendingSpecification(), _cancellationToken);

        Assert.Equal("Temperature", orderedByNameDescPhysicalDimensions[0].Name);

        Assert.Equal(2, await physicalDimensionRepository.CountAsync(new OrderPhysicalDimensionByNameDescendingSpecification(), _cancellationToken));

        System.Collections.Generic.List<PhysicalDimension> paginatedPhysicalDimensions = await physicalDimensionRepository.ListAsync(new PaginationSpecification(2, 1), _cancellationToken);

        Assert.Single(paginatedPhysicalDimensions);

        var sensorRepository = new SensorRepository(context);

        var sensor = new Sensor()
        {
            Name = "TemperatureSensor",
            Make = "Maker",
            Model = "Model one",
            SerialNumber = "Serial number one",
            PhysicalDimension = await physicalDimensionRepository.FirstOrDefaultAsync(new GetPhysicalDimensionByNameSpecification("Temperature"), _cancellationToken)
                                 ?? throw new InvalidOperationException("PhysicalDimension not found"),
            Created = DateTime.UtcNow,
            CreatedBy = "atech"
        };

        await sensorRepository.AddAsync(sensor, _cancellationToken);
        await sensorRepository.SaveChangesAsync(_cancellationToken);

        System.Collections.Generic.List<Sensor> sensors = await sensorRepository.ListAsync(new GetSensorByNameWithPhysicalDimensionSpecification("TemperatureSensor"), _cancellationToken);
        Assert.True(sensors.Count > 0);

        Assert.NotNull(sensors[0].PhysicalDimension);
        Assert.Equal("Temperature", sensors[0].PhysicalDimension.Name);

        // Update the sensor
        var sensorId = sensors[0].Id;
        var sensorToUpdate = await sensorRepository.FirstOrDefaultAsync(new SensorByIdSpecification(sensorId), _cancellationToken);

        Assert.NotNull(sensorToUpdate);

        sensorToUpdate.Name = "UpdatedSensorName";
        sensorToUpdate.Make = "UpdatedMake";
        sensorToUpdate.Model = "UpdatedModel";
        sensorToUpdate.SerialNumber = "UpdatedSerialNumber";

        await sensorRepository.UpdateAsync(sensorToUpdate, _cancellationToken);
        await sensorRepository.SaveChangesAsync(_cancellationToken);

        var updatedSensor = await sensorRepository.FirstOrDefaultAsync(new SensorByIdSpecification(sensorId), _cancellationToken);
        Assert.NotNull(updatedSensor);
        Assert.Equal("UpdatedSensorName", updatedSensor.Name);
        Assert.Equal("UpdatedMake", updatedSensor.Make);
        Assert.Equal("UpdatedModel", updatedSensor.Model);
        Assert.Equal("UpdatedSerialNumber", updatedSensor.SerialNumber);
    }
}

internal sealed class SensorByIdSpecification : Specification<Sensor>
{
    public SensorByIdSpecification(int id) : base(criteria: c => true)
    {
        Criteria = Criteria.And(c => c.Id == id);

        ApplyNoTracking();
     }
}
