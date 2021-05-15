# ATech.Repository

Developing modern applications frequently requires some kind of data handling. Whether the application is sized to fit the resources of a tiny microcontroller to collect some IoT data, or implements a more sophisticated microservice or a big enterprise business application, it will always require some kind of data storage or retrieval, in other words, needs some CRUD operations. Achieving such a goal is possible in many ways. We can interact with the data storage (whether it's a database or not) directly with a dedicated controller ORM or Micro ORM, or we can use a dedicated layer to abstract data access from the implementation logic. As stated by Martin Fowler a [Repository](https://martinfowler.com/eaaCatalog/repository.html) *Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects*, The repository should look like an in-memory collection and should have generic methods like Add, Remove or FindById. With such generic methods, the repository can be easily reused in different applications.
This project is my own interpretation of this particular design pattern resumes many of the articles I've read on the subject, and it's available as a set of nuget packages [https://www.nuget.org/packages?q=ATech.Repository](https://www.nuget.org/packages?q=ATech.Repository)
  
## ATech.Repository.EntityFrameworkCore

The natural solution for relational databases integration in a .NET Core application is, without any doubt, [Entity Framework Core](https://entityframeworkcore.com/).

Like everything, EF core also has its strengths and weaknesses. It is certainly not the lightest of the solutions available nor the fastest, but the possibility of adopting a code first approach is for me a plus in the early stages of a project, allowing me to focus on the architectural aspects of the solution.

- [Implement the infrastructure persistence layer with Entity Framework Core](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)

- [Repository Pattern with C# and Entity Framework, Done Right](https://youtu.be/rtXpYpZdOzM)

## ATech.Repository.Dapper

Remaining in the field of relational databases, [Dapper](https://dapperlib.github.io/Dapper/) is the undisputed protagonist in the dotnet community. It's lightweight, it's much faster than EF Core (at least in reading operations), but in my opinion, using it as it comes at an unacceptable cost. Writing SQL statements directly into code even for simple operations will significantly slow down the development phase.
This my attempt to implement the Repository Pattern and add some levels of abstraction to this wonderful tool.

- [Dapper ORM](https://dapper-tutorial.net)
- [Dapper vs EF Core Query Performance Benchmarking](https://exceptionnotfound.net/dapper-vs-entity-framework-core-query-performance-benchmarking-2019/)