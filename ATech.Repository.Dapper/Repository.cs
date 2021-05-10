using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.Dapper.Extensions;

namespace ATech.Repository.Dapper
{
#pragma warning disable CS1591
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection connection;

        public Repository(IDbConnection connection)
            => this.connection = connection ?? throw new ArgumentNullException(nameof(connection));

        public void Add(TEntity entity)
            => connection.Insert<TEntity>(entity);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => await connection.InsertAsync<TEntity>(entity);

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    connection.Insert<TEntity>(e);
                }
            }
        }

        public int Count()
            => connection.Count<TEntity>();

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
            => connection.Find<TEntity>(predicate);

        public TEntity Get(int id)
            => connection.Get<TEntity>(id);

        public TEntity Get(Guid id)
            => connection.Get<TEntity>(id);

        public IEnumerable<TEntity> GetAll()
            => connection.GetAll<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await connection.GetAllAsync<TEntity>(cancellationToken);

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
            => await connection.GetAsync<TEntity>(id, cancellationToken);

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
            => await connection.GetAsync<TEntity>(id, cancellationToken);

        public void Update(TEntity entity) 
            => connection.Update<TEntity>(entity);
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken) 
            => await connection.UpdateAsync<TEntity>(entity, cancellationToken);

        public void Remove(TEntity entity)
            => connection.Delete<TEntity>(entity);


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    connection.Delete<TEntity>(e);
                }
            }
        }
    }
#pragma warning restore CS1591
}
