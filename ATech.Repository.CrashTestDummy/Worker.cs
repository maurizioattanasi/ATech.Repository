using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.CrashTestDummy.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ATech.Repository.CrashTestDummy
{
    public class Worker : IHostedService, IDisposable
    {
        private SqlConnection connection;
        private readonly ILogger<Worker> _logger;
        private bool disposed = false;

        public Worker(SqlConnection connection,
            ILogger<Worker> logger)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger;
        }

        public async Task WorkerFunction(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var unitOfWork = new IoTDataMartDbUnitOfWork(ref connection);

            int count = unitOfWork
                .Sensors
                .Count();

            _logger.LogInformation("There are {count} sensors", count);

            var sensor = await unitOfWork
                .Sensors
                .GetAsync(1, cancellationToken)
                .ConfigureAwait(false);

            if (sensor != null)
                _logger.LogInformation("Sensor found {sensor}", JsonSerializer.Serialize(sensor));

            var sensors = await unitOfWork
                .Sensors
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);

            if (sensors != null)
            {
                _logger.LogInformation("Found {count} sensors", sensors.Count());
                foreach (var s in sensors)
                {
                    _logger.LogInformation("Sensor found {sensor}", JsonSerializer.Serialize(s));
                }
            }

            var dimensions = await unitOfWork
                .PhysicalDimensions
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);

            if (dimensions != null)
            {
                _logger.LogInformation("Found {count} physical dimensions", dimensions.Count());
                foreach (var d in dimensions){
                    _logger.LogInformation("Removing {dimension}", JsonSerializer.Serialize(d));
                    unitOfWork
                        .PhysicalDimensions
                        .Remove(d);
                }
            }

            unitOfWork
                .PhysicalDimensions
                .Add(new Entities.PhysicalDimension
                {
                    Name = "Humidity",
                    Scale = "%",
                    Created = DateTime.UtcNow,
                    CreatedBy = "atech"
                });

            dimensions = await unitOfWork
                .PhysicalDimensions
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);

            unitOfWork
                .PhysicalDimensions
                .RemoveRange(dimensions);

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
            }

            disposed = true;
        }
    }
}
