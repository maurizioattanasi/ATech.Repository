using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.EntityFrameworkCore;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private readonly DbContext _context;

    public Repository(DbContext context)
        => _context = context;

    /// <inheritdoc/>
    public virtual TEntity? GetById(TId id)
        => _context.Set<TEntity>().Find(id);

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id), "Id cannot be null.");
        }

        return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> GetAll()
        => _context.Set<TEntity>();

    /// <inheritdoc/>
    public virtual async ValueTask<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await Task.Run(() => _context.Set<TEntity>(), cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public IEnumerable<TEntity> Missing(IEnumerable<TEntity> toExclude, IEqualityComparer<TEntity>? comparer)
        => toExclude.Except(_context.Set<TEntity>(), comparer);

    /// <inheritdoc/>
    public virtual void Add(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

    /// <inheritdoc/>
    public virtual async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public virtual void AddRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().AddRange(entities);

    /// <inheritdoc/>
    public virtual async ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public virtual void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    /// <inheritdoc/>
    public virtual void RemoveRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);

    /// <inheritdoc/>
    public int Count()
        => _context.Set<TEntity>().Count();

    /// <inheritdoc/>
    public async ValueTask<int> CountAsync(CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().CountAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).CountAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    /// <inheritdoc/>
    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        => await Task.Run(() => _context.Set<TEntity>().Update(entity), cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public int SaveChanges() => _context.SaveChanges();

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
       => await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).ToListAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default)
         => await _context.Set<TEntity>().AnyAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async ValueTask<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
       => await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).AnyAsync(cancellationToken).ConfigureAwait(false);
}