using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository;

/// <summary>
/// Defines a generic repository interface for interacting with a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity the repository will handle.</typeparam>
/// <typeparam name="TId">The type of the primary key for the entity.</typeparam>
public interface IRepository<TEntity, TId> : IReadRepository<TEntity, TId> where TEntity : class
{
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
