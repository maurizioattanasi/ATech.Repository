using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ATech.Repository.Dapper.Extensions;
using Dapper;


namespace ATech.Repository.Dapper
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection connection;

        public Repository(IDbConnection connection) => this.connection = connection ?? throw new ArgumentNullException(nameof(connection));

        private string TableName { get { return typeof(TEntity).Name; } }

        public void Add(TEntity entity)
        {
            connection.Insert<TEntity>(entity);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {            
            await connection.InsertAsync<TEntity>(entity);
        }

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

        public int Count() => connection.QueryFirst<int>($"SELECT COUNT(*) FROM {TableName}");

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id) => connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id });

        public TEntity Get(Guid id) => connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id });

        public IEnumerable<TEntity> GetAll() => connection.Query<TEntity>($"SELECT * FROM {TableName}");

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => await connection.QueryAsync<TEntity>($"SELECT * FROM {TableName}");

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken) => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id });

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken) => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id });

        public void Remove(TEntity entity) => connection.Delete<TEntity>(entity);

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
