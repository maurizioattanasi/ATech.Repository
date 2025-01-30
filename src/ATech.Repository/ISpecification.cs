using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ATech.Repository;

public interface ISpecification<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }
   
    /// <summary>
    /// Gets a list of expressions representing the properties to include in the query results.
    /// </summary>
    /// <remarks>
    /// This list is used to specify the related entities to be included in the query results.
    /// By including related entities, you can reduce the number of database queries and improve performance.
    /// </remarks>
    ReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }

    /// <summary>
    /// Gets the expression to order the entities in ascending order.
    /// </summary>
    /// <remarks>
    /// This expression is used to specify the property to order the entities by when retrieving data from the repository.
    /// </remarks>
    Expression<Func<TEntity, object>> OrderBy { get; }

    /// <summary>
    /// Gets the expression to order the entities in descending order.
    /// </summary>
    /// <remarks>
    /// This expression is used to specify the property to order the entities by when retrieving data from the repository.
    /// </remarks>
    
    Expression<Func<TEntity, object>> OrderByDescending { get; }

    /// <summary>
    /// The number of elements to return.
    /// </summary>
    int? Take { get; }

    /// <summary>
    /// The number of elements to skip before returning the remaining elements.
    /// </summary>
    int? Skip { get; }

    /// <summary>
    /// Indicates whether the query should be executed with "AsNoTracking" option.
    /// </summary>
    bool AsNoTracking { get; }
}

