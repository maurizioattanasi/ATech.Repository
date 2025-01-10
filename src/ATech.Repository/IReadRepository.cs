using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository;

/// <summary>
/// Interface for read-only repository operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public interface IReadRepository<TEntity, TId> where TEntity : class
{
    /// <summary>
    /// Retrieves an entity from the data store based on the provided primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <returns>The entity with the specified primary key, or null if not found.</returns>
    TEntity? GetById(TId id);

    /// <summary>
    /// Retrieves an entity from the data store based on the provided primary key asynchronously.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be the entity with the specified primary key, or null if not found.
    /// </returns>
    ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first entity that satisfies the specified specification, or a default value if no such entity is found.
    /// </summary>
    /// <param name="specification">The specification that the entity must satisfy.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be the first entity that satisfies the specified specification, or null if no such entity is found.
    /// </returns>
    ValueTask<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first entity that satisfies the specified specification, or a default value if no such entity is found.
    /// </summary>
    /// <param name="specification">The specification that the entity must satisfy.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be the first entity that satisfies the specified specification, or null if no such entity is found.
    /// </returns>
    ValueTask<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default); 
    
    /// <summary>
    /// Retrieves a list of entities from the data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be a List of entities retrieved from the data store.
    /// If no entities are found, an empty List will be returned.
    /// </returns>
    ValueTask<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves a list of entities from the data store asynchronously based on the provided specification.
    /// </summary>
    /// <param name="specification">
    /// The specification that the entities must satisfy.
    /// This parameter can be used to filter, sort, or paginate the results.
    /// If no specification is provided, all entities will be retrieved.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be a List of entities retrieved from the data store that satisfy the provided specification.
    /// If no entities are found, an empty List will be returned.
    /// </returns>
    ValueTask<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously checks if any entities exist in the data store.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be a boolean value indicating whether any entities exist in the data store.
    /// If at least one entity is found, the method will return true. Otherwise, it will return false.
    /// </returns>
    ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously checks if any entities exist in the data store that satisfy the provided specification.
    /// </summary>
    /// <param name="specification">
    /// The specification that the entities must satisfy.
    /// This parameter can be used to filter, sort, or paginate the results.
    /// If no specification is provided, the method will check for any entities in the data store.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be a boolean value indicating whether any entities exist in the data store that satisfy the provided specification.
    /// If at least one entity is found, the method will return true. Otherwise, it will return false.
    /// </returns>
    ValueTask<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the total number of entities in the data store.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be an integer representing the total number of entities in the data store.
    /// </returns>
    /// <remarks>
    /// This method does not execute a query against the data store.
    /// Instead, it returns the count of entities that would be returned by a query.
    /// This count is typically maintained by the underlying data store and updated whenever entities are added, updated, or removed.
    /// </remarks>
    ValueTask<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves the total number of entities in the data store that satisfy the provided specification.
    /// </summary>
    /// <param name="specification">
    /// The specification that the entities must satisfy.
    /// This parameter can be used to filter, sort, or paginate the results.
    /// If no specification is provided, the function will return the total count of entities in the data store.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled as well.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be an integer representing the total number of entities in the data store that satisfy the provided specification.
    /// </returns>
    /// <remarks>
    /// This method does not execute a query against the data store.
    /// Instead, it returns the count of entities that would be returned by a query based on the provided specification.
    /// This count is typically maintained by the underlying data store and updated whenever entities are added, updated, or removed.
    /// </remarks>
    ValueTask<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities from the data store.
    /// </summary>
    /// <returns>
    /// An IQueryable of all entities in the data store.
    /// If no entities are found, an empty IQueryable is returned.
    /// </returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Retrieves all entities from the data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be an IEnumerable of all entities in the data store.
    /// If no entities are found, an empty IEnumerable is returned.
    /// </returns>
    ValueTask<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the total number of entities in the data store.
    /// </summary>
    /// <returns>
    /// An integer representing the total number of entities in the data store.
    /// </returns>
    /// <remarks>
    /// This method does not execute a query against the data store.
    /// Instead, it returns the count of entities that would be returned by a query.
    /// This count is typically maintained by the underlying data store and updated whenever entities are added, updated, or removed.
    /// </remarks>
    int Count();

    /// <summary>
    /// Retrieves a collection of entities from the provided collection that are not present in the data store.
    /// </summary>
    /// <param name="toExclude">A collection of entities to exclude from the result.</param>
    /// <param name="comparer">
    /// An optional implementation of the IEqualityComparer&lt;TEntity&gt; interface to use for comparing entities.
    /// If not provided, the default equality comparer for the TEntity type will be used.
    /// </param>
    /// <returns>
    /// An IEnumerable of entities that are not present in the provided collection.
    /// If no entities are found that are not present in the provided collection, an empty IEnumerable is returned.
    /// </returns>
    IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer);
}
