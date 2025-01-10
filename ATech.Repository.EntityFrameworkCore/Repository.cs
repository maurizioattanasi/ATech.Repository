using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.EntityFrameworkCore;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    public readonly DbContext _context;

    public Repository(DbContext context)
        => _context = context;

    public virtual TEntity? GetById(TId id)
        => _context.Set<TEntity>().Find(id);

    public virtual async ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be null.");
        }
        
        return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual IQueryable<TEntity> GetAll()
        => _context.Set<TEntity>();

    public virtual async ValueTask<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await Task.Run(() => _context.Set<TEntity>(), cancellationToken);

    public IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer)
        => toExclude.Except(_context.Set<TEntity>(), comparer);

    public virtual void Add(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

    public virtual async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public virtual void AddRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().AddRange(entities);

    public virtual async ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().AddRange(entities);
        await Task.Yield();
    }

    public virtual void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);

    public int Count()
        => _context.Set<TEntity>().Count();

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        => await Task.Run(() => _context.Set<TEntity>().Update(entity), cancellationToken);

    public int SaveChanges() => _context.SaveChanges();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public ValueTask<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<int> AnyAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<int> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}