using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.Dapper.Extensions;

namespace ATech.Repository.Dapper;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
{
    private readonly IDbConnection _connection;

    public Repository(IDbConnection connection)
        => this._connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public TEntity? Get(TId id)
        => _connection.Get<TEntity, TId>(id);

    public async ValueTask<TEntity?> GetAsync(TId id, CancellationToken cancellationToken)
        => await _connection.GetAsync<TEntity, TId>(id, cancellationToken);

    public IQueryable<TEntity> GetAll()
        => _connection.GetAll<TEntity>().AsQueryable();

    public async ValueTask<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => (await _connection.GetAllAsync<TEntity>(cancellationToken)).AsQueryable();

    public IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer)
        => toExclude.Except(_connection.GetAll<TEntity>(), comparer);

    public void Add(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        _connection.Insert<TEntity>(entity);
    }

    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        await _connection.InsertAsync<TEntity>(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        if (entities != null)
        {
            foreach (var e in entities)
            {
                if(e is null) continue;
                _connection.Insert<TEntity>(e);
            }
        }
    }

    public virtual async ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        _connection.Delete<TEntity>(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        if (entities != null)
        {
            foreach (var e in entities)
            {
                if(e is null) continue;
                _connection.Delete<TEntity>(e);
            }
        }
    }

    public void Update(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        _connection.Update<TEntity>(entity);
    }

    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        await _connection.UpdateAsync<TEntity>(entity, cancellationToken);
    }

    public int Count()
        => _connection.Count<TEntity>();

    public int SaveChanges() => 0;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(SaveChanges());
}


