using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository;

public interface IRepository<TEntity, TId>
{
    TEntity Get(TId id);
    ValueTask<TEntity> GetAsync(TId id, CancellationToken cancellationToken);

    IEnumerable<TEntity> GetAll();
    ValueTask<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

    void Add(TEntity entity);
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    ValueTask    UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    int Count();
}
