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
public interface IReadRepository<TEntity, TId>
{
    /// <summary>
    /// Retrieves an entity from the data store based on the provided primary key.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <returns>The entity with the specified primary key, or null if not found.</returns>
    TEntity? Get(TId id);

    /// <summary>
    /// Retrieves an entity from the data store based on the provided primary key asynchronously.
    /// </summary>
    /// <param name="id">The primary key of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be the entity with the specified primary key, or null if not found.
    /// </returns>
    ValueTask<TEntity?> GetAsync(TId id, CancellationToken cancellationToken);

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
