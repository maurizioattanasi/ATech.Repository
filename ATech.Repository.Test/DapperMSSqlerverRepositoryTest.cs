using System;
using Xunit;
using System.Data;
using System.Data.SqlClient;
using ATech.Repository.Test.Repository;
using System.Linq;

namespace ATech.Repository.Test
{
    public class DapperMSSqlerverRepositoryTest
    {
        private IDbConnection connection;

        public DapperMSSqlerverRepositoryTest()
        {
            var connectionString = "Data Source=127.0.0.1,6433;Initial Catalog=ATech.IoTDataMart;User ID=sa;Password=Pass@word;Persist Security Info=True;";
            connection = new SqlConnection(connectionString);
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
                .GetAllAsync(default)
                .ConfigureAwait(false);

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
                   .GetAllAsync(default)
                   .ConfigureAwait(false);

            var row = rows.ToArray()[0];

            var retrieved = await unitOfWork
                .PhysicalDimensions
                .GetAsync(row.Id, default)
                .ConfigureAwait(false);

            retrieved.Name = "Humidity0123";

            await unitOfWork
                .PhysicalDimensions
                .UpdateAsync(retrieved, default)
                .ConfigureAwait(false);

            var dimension = unitOfWork.PhysicalDimensions.Find(d => d.Name.ToLower() == "humidity0123").FirstOrDefault();

            Assert.True((dimension != null) && (dimension.Name == "Humidity0123"));

            rows = await unitOfWork
                   .PhysicalDimensions
                   .GetAllAsync(default)
                   .ConfigureAwait(false);

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
