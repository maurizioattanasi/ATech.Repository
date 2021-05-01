using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.EntityFrameworkCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DbContext Context;

        public Repository(DbContext context) => Context = context;

        public virtual TEntity Get(int id) => this.Context.Set<TEntity>().Find(id);

        public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken) => await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

        public virtual async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken) => await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

        public virtual TEntity Get(Guid id) => this.Context.Set<TEntity>().Find(id);

        public virtual IEnumerable<TEntity> GetAll() => this.Context.Set<TEntity>().ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => await this.Context.Set<TEntity>().ToListAsync(cancellationToken);

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => this.Context.Set<TEntity>().Where(predicate);

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => this.Context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync(cancellationToken);

        public virtual void Add(TEntity entity) => this.Context.Set<TEntity>().Add(entity);

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken) => await this.Context.Set<TEntity>().AddAsync(entity, cancellationToken);

        public virtual void AddRange(IEnumerable<TEntity> entities) => this.Context.Set<TEntity>().AddRange(entities);

        public virtual void Remove(TEntity entity) => this.Context.Set<TEntity>().Remove(entity);

        public virtual void RemoveRange(IEnumerable<TEntity> entities) => this.Context.Set<TEntity>().RemoveRange(entities);

        public int Count() => this.Context.Set<TEntity>().Count();
    }
}
