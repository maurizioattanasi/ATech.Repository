using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.EntityFrameworkCore
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        public readonly DbContext _context;

        public Repository(DbContext context) 
            => _context = context;

        public virtual TEntity Get(TId id) 
            => _context.Set<TEntity>().Find(id);

        public virtual async ValueTask<TEntity> GetAsync(TId id, CancellationToken cancellationToken) 
            => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

        public virtual TEntity Get(Guid id) 
            => _context.Set<TEntity>().Find(id);

        public virtual IEnumerable<TEntity> GetAll() 
            => _context.Set<TEntity>().ToList();

        public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
            => await _context.Set<TEntity>().ToListAsync(cancellationToken);

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate) 
            => _context.Set<TEntity>().Where(predicate);

        public virtual void Add(TEntity entity) 
            => _context.Set<TEntity>().Add(entity);

        public virtual async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken) 
            => await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

        public virtual void AddRange(IEnumerable<TEntity> entities) 
            => _context.Set<TEntity>().AddRange(entities);

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

    }
}
