using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.Dapper.Extensions;

namespace ATech.Repository.Dapper
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection connection;

        public Repository(IDbConnection connection) => this.connection = connection ?? throw new ArgumentNullException(nameof(connection));

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

        public int Count() => connection.Count<TEntity>();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

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
}
