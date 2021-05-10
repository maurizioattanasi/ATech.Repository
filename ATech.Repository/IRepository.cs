using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ATech.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);

        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();
    }
}
