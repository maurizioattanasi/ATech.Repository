using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository;

/// <summary>
/// Defines a generic repository interface for interacting with a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity the repository will handle.</typeparam>
/// <typeparam name="TId">The type of the primary key for the entity.</typeparam>
public interface IRepository<TEntity, TId>
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
    /// An IEnumerable of all entities in the data store.
    /// If no entities are found, an empty IEnumerable is returned.
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
    /// Retrieves a collection of entities from the data store based on the provided predicate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// An IEnumerable of entities that satisfy the condition defined by the specified predicate.
    /// If no entities satisfy the condition, an empty IEnumerable is returned.
    /// </returns>
    IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);


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

    /// <summary>
    /// Adds a new entity to the data store.
    /// </summary>
    /// <param name="entity">The entity to add to the data store.</param>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// </remarks>
    void Add(TEntity entity);

    /// <summary>
    /// Adds a new entity to the data store asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add to the data store.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The task will complete once the entity has been added to the data store.
    /// If the operation is canceled, the ValueTask will be canceled.
    /// </returns>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// </remarks>
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a collection of new entities to the data store.
    /// </summary>
    /// <param name="entities">The collection of entities to add to the data store.</param>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// If the provided collection is null or empty, this method does nothing.
    /// </remarks>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds a collection of new entities to the data store asynchronously.
    /// </summary>
    /// <param name="entities">The collection of entities to add to the data store.</param>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The task will complete once all the entities have been added to the data store.
    /// </returns>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// If the provided collection is null or empty, this method does nothing.
    /// </remarks>
    ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);


    /// <summary>
    /// Updates an existing entity in the data store with the properties of the provided entity.
    /// </summary>
    /// <param name="entity">The entity with the updated properties to apply to the existing entity in the data store.</param>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// If the provided entity does not exist in the data store, this method does nothing.
    /// </remarks>
    void Update(TEntity entity);


    /// <summary>
    /// Updates an existing entity in the data store with the properties of the provided entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity with the updated properties to apply to the existing entity in the data store.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The task will complete once the entity has been updated in the data store.
    /// If the provided entity does not exist in the data store, the ValueTask will not complete.
    /// If the operation is canceled, the ValueTask will be canceled.
    /// </returns>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// </remarks>
    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Removes the specified entity from the data store.
    /// </summary>
    /// <param name="entity">The entity to remove from the data store.</param>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// If the provided entity does not exist in the data store, this method does nothing.
    /// </remarks>
    void Remove(TEntity entity);

    /// <summary>
    /// Removes a collection of specified entities from the data store.
    /// </summary>
    /// <param name="entities">The collection of entities to remove from the data store.</param>
    /// <remarks>
    /// This method does not save the changes to the data store immediately.
    /// To persist the changes, call the SaveChanges() or SaveChangesAsync() method.
    /// If the provided collection is null or empty, this method does nothing.
    /// If any of the provided entities do not exist in the data store, they will not be removed.
    /// </remarks>
    void RemoveRange(IEnumerable<TEntity> entities);

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
    /// Saves all changes made in this context to the underlying database.
    /// </summary>
    /// <returns>
    /// The number of state entries written to the database. This can include
    /// state entries for entities and/or relationships. The specific number of entries
    /// written to the database may vary depending on the changes made and the specific
    /// conditions of the database and connection.
    /// </returns>
    /// <remarks>
    /// This method will automatically call <see cref="DbContext.DetectChanges"/> to discover any changes
    /// to entity instances before saving to the underlying database. This can be disabled via
    /// <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
    /// 
    /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
    /// that any asynchronous operations have completed before calling another method on this context.
    /// </remarks>
    int SaveChanges();

    /// <summary>
    /// /// Saves all changes made in this context to the underlying database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token to cancel the operation.
    /// If the operation is canceled, the returned ValueTask will be canceled.
    /// </param>
    /// <returns>
    /// A ValueTask that represents the asynchronous operation.
    /// The result of the ValueTask will be an integer representing the number of state entries written to the database.
    /// This can include state entries for entities and/or relationships.
    /// The specific number of entries written to the database may vary depending on the changes made and the specific conditions of the database and connection.
    /// </returns>
    /// <remarks>
    /// This method will automatically call <see cref="DbContext.DetectChanges"/> to discover any changes
    /// to entity instances before saving to the underlying database. This can be disabled via
    /// <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
    /// 
    /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
    /// that any asynchronous operations have completed before calling another method on this context.
    /// </remarks>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
