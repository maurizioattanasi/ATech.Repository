using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.Dapper.Extensions;

namespace ATech.Repository.Dapper;

#pragma warning disable CS1591
public class Repository<TEntity, TId> : IRepository<TEntity, TId>
{
    private readonly IDbConnection _connection;

    public Repository(IDbConnection connection)
        => this._connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public TEntity Get(TId id)
        => _connection.Get<TEntity, TId>(id);

    public async ValueTask<TEntity> GetAsync(TId id, CancellationToken cancellationToken)
        => await _connection.GetAsync<TEntity, TId>(id, cancellationToken);

    public IEnumerable<TEntity> GetAll()
        => _connection.GetAll<TEntity>();

    public async ValueTask<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await _connection.GetAllAsync<TEntity>(cancellationToken);

    public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        => _connection.Find<TEntity>(predicate);

    public IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity> comparer)
    {
        throw new NotImplementedException();
    }

    public void Add(TEntity entity)
        => _connection.Insert<TEntity>(entity);

    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _connection.InsertAsync<TEntity>(entity);

    public void AddRange(IEnumerable<TEntity> entities)
    {
        if (entities != null)
        {
            foreach (var e in entities)
            {
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
        => _connection.Delete<TEntity>(entity);

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        if (entities != null)
        {
            foreach (var e in entities)
            {
                _connection.Delete<TEntity>(e);
            }
        }
    }

    public void Update(TEntity entity)
        => _connection.Update<TEntity>(entity);
    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        => await _connection.UpdateAsync<TEntity>(entity, cancellationToken);

    public int Count()
        => _connection.Count<TEntity>();

    public int SaveChanges() => 0;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(SaveChanges());
}
#pragma warning restore CS1591

