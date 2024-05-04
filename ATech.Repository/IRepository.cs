using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository;

public interface IRepository<TEntity, TId>
{
    TEntity? Get(TId id);
    ValueTask<TEntity?> GetAsync(TId id, CancellationToken cancellationToken);

    IEnumerable<TEntity> GetAll();
    ValueTask<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

    IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer);

    void Add(TEntity entity);
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken);

    void AddRange(IEnumerable<TEntity> entities);
    ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    int Count();

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
