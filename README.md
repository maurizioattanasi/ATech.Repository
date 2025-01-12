# ATech.Repository

Developing modern applications frequently requires some kind of data handling. Whether the application is sized to fit the resources of a tiny microcontroller to collect some IoT data, or implements a more sophisticated microservice or a big enterprise business application, it will always require some kind of data storage or retrieval, in other words, needs some CRUD operations. Achieving such a goal is possible in many ways. We can interact with the data storage (whether it's a database or not) directly with a dedicated controller, ORM or Micro ORM, or we can use a dedicated layer to abstract data access from the implementation logic implementing a *Repository Pattern*.

As stated by Martin Fowler a [Repository](https://martinfowler.com/eaaCatalog/repository.html) *Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects*, The repository should look like an in-memory collection and should have generic methods like Add, Remove or FindById. With such generic methods, the repository can be easily reused in different applications.

![Repository Pattern](./img/Repository-pattern-UML-diagram.jpeg)

This project is my own interpretation of this particular design pattern resumes many of the articles I've read on the subject, and it's available as a set of nuget packages [https://www.nuget.org/packages?q=ATech.Repository](https://www.nuget.org/packages?q=ATech.Repository)
  
## ATech.Repository.EntityFrameworkCore

The natural solution for relational databases integration in a .NET Core application is, without any doubt, [Entity Framework Core](https://entityframeworkcore.com/).

Like everything, EF core also has its strengths and weaknesses. It is certainly not the lightest of the solutions available nor the fastest, but the possibility of adopting a code first approach is for me a plus in the early stages of a project, allowing to focus on the architectural aspects of the solution.

The EF Core version of this pattern is used in [ATech.ContactFormServer](https://github.com/maurizioattanasi/ATech.ContactFormServer) service in which the Domain project contains the interfaces of two concrete POCO classes, while the Infrastructure project contains the Implementations.

### References

- [Implement the infrastructure persistence layer with Entity Framework Core](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)

- [Repository Pattern with C# and Entity Framework, Done Right](https://youtu.be/rtXpYpZdOzM)

## ATech.Repository.Dapper

Remaining in the field of relational databases, [Dapper](https://dapperlib.github.io/Dapper/) is one of the undisputed protagonists in the dotnet community. It's lightweight, it's much faster than EF Core (at least in reading operations), but in my opinion, using it as it comes has an unacceptable cost. Writing SQL statements directly into code even for simple operations will significantly slow down the development phase.
This is my attempt to implement the repository model and add some layers of abstraction to this remarkable tool.

Mimicking EF Core, I assumed that each entity class has a corresponding table with the same name in the database and, for simplicity (perhaps oversimplified), that each table has a primary key column named Id.

The core of the repository implementation consists of a number of *IDbConnection* extension methods, contained in [DapperExtensions.cs](./atech.repository.dapper/../ATech.Repository.Dapper/Extensions/DapperExtensions.cs) file, each of which creates and executes a corresponding parameterized SQL query.

```C#
    /// <summary>
    /// Generic asynchronous Read extension method
    /// </summary>        
    /// <param name="id">Unique id</param>        
    /// <returns>The item corresponding to the given id if exists</returns>
    public static async Task<TEntity> GetAsync<TEntity>(this IDbConnection connection, int id,      CancellationToken cancellationToken)
            => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id", new { Id = id });
```

An example of how the use of the class is contained in the **ATech.Repository.Test** project. it's made up of three domain entities (POCO Classes), each with its concrete repository interface and implementation. As it almost always happens repositories comes with unit of work that, as stated by Martin *Maintains a list of objects affected by a business transaction and coordinates the writing out of changes and the resolution of concurrency problems.* 

```C#
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
```

### References

- [Dapper ORM](https://dapper-tutorial.net)
- [Dapper vs EF Core Query Performance Benchmarking](https://exceptionnotfound.net/dapper-vs-entity-framework-core-query-performance-benchmarking-2019/)

## Releases

- 2.0.0.0 - 2.0 package refactoring
- 1.3.0.0 - Migration to .NET 8
- 3.0.0 - Migration to .NET 9 and 