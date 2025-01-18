using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


using ATech.Repository.Dapper.Extensions;

namespace ATech.Repository.Dapper;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private readonly IDbConnection _connection;

    public Repository(IDbConnection connection)
        => this._connection = connection ?? throw new ArgumentNullException(nameof(connection));

    public TEntity? GetById(TId id)
        => _connection.Get<TEntity, TId>(id);

    public async ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        => await _connection.GetAsync<TEntity, TId>(id).ConfigureAwait(false);

    public IQueryable<TEntity> GetAll()
        => _connection.GetAll<TEntity>().AsQueryable();

    public async ValueTask<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => (await _connection.GetAllAsync<TEntity>().ConfigureAwait(false)).AsQueryable();

    public IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer)
        => toExclude.Except(_connection.GetAll<TEntity>(), comparer);

    public void Add(TEntity entity)
        => _connection.Insert<TEntity>(entity);

    public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _connection.InsertAsync<TEntity>(entity).ConfigureAwait(false);

    public void AddRange(IEnumerable<TEntity> entities)
    {
        if (entities != null)
        {
            foreach (TEntity? e in entities)
            {
                if (e is null)
                {
                    continue;
                }

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
            foreach (TEntity? e in entities)
            {
                if (e is null)
                {
                    continue;
                }

                _connection.Delete<TEntity>(e);
            }
        }
    }

    public void Update(TEntity entity)
        => _connection.Update<TEntity>(entity);

    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        => await _connection.UpdateAsync<TEntity>(entity).ConfigureAwait(false);

    public int Count()
        => _connection.Count<TEntity>();

    public int SaveChanges() => 0;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(SaveChanges());

    public ValueTask<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<int> CountAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) => throw new NotImplementedException();

}


