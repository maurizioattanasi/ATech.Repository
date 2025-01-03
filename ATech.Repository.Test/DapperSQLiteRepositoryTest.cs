using System;
using Xunit;
using System.Data;
using ATech.Repository.Test.Repository;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace ATech.Repository.Test
{
    public class DapperSQLiteRepositoryTest
    {
        private IDbConnection connection;

        public DapperSQLiteRepositoryTest()
        {
            var connectionString = "Data Source=../../../data/ATech.IoTDataMart.db";
            connection = new SqliteConnection(connectionString);
        }

        /// <summary>
        /// Check the creation feature of the repository
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CRUDTest()
        {
            var unitOfWork = new IoTDataMartDbUnitOfWork(ref connection);

            var rows = await unitOfWork
                .PhysicalDimensions
                .GetAllAsync(default);

            unitOfWork
                .PhysicalDimensions
                .RemoveRange(rows);

            // Count the rows before Create operation
            var before = unitOfWork
                .PhysicalDimensions
                .Count();

            // Adds a new row
            unitOfWork
                .PhysicalDimensions
                .Add(new Entities.PhysicalDimension
                {
                    Name = "Humidity",
                    Scale = "%",
                    Created = DateTime.UtcNow,
                    CreatedBy = "atech"
                });

            var after = unitOfWork
                .PhysicalDimensions
                .Count();

            Assert.True(after == before + 1);

            rows = await unitOfWork
                   .PhysicalDimensions
                   .GetAllAsync(default);

            var row = rows.ToArray()[0];

            var retrieved = await unitOfWork
                .PhysicalDimensions
                .GetAsync(row.Id, default);

            retrieved.Name = "Humidity0123";

            await unitOfWork
                .PhysicalDimensions
                .UpdateAsync(retrieved, default);

            var dimension = unitOfWork.PhysicalDimensions.GetAll().Where(d => d.Name.ToLower() == "humidity0123").FirstOrDefault();

            Assert.True((dimension != null) && (dimension.Name == "Humidity0123"));

            rows = await unitOfWork
                   .PhysicalDimensions
                   .GetAllAsync(default);

            foreach (var r in rows)
            {
                unitOfWork
                   .PhysicalDimensions
                   .Remove(r);
            }

            after = unitOfWork
                    .PhysicalDimensions
                    .Count();

            Assert.True(after == 0);
        }
    }
}
