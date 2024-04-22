using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace ATech.Repository.Dapper.Extensions
{
    /// <summary>
    /// Dapper extension methods for basic CRUD operations
    /// </summary>
    public static class DapperExtensions
    {
        /// <summary>
        /// Insert query string builder
        /// </summary>
        /// <param name="entity">generic POCO class instance</param>        
        /// <returns>The insert query string</returns>
        private static string BuildInsertQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Where(p => p.Name.ToLower() != "id").Select(p => p.Name).ToArray();

            var query = string.Format("INSERT INTO {0} ({1}) VALUES (@{2})",
                                             tableName,
                                             string.Join(",", columns),
                                             string.Join(",@", columns));
            return query;
        }

        /// <summary>
        /// Update query string builder
        /// </summary>
        /// <param name="entity">generic POCO class instance</param>
        /// <returns>The update query string</returns>
        private static string BuildUpdateQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Where(p => p.Name.ToLower() != "id").Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            var query = string.Format("UPDATE {0} SET {1} WHERE Id=@Id", tableName, string.Join(",", parameters));

            return query;
        }

        /// <summary>
        /// Deltion query string builder
        /// </summary>
        /// <param name="entity">generic POCO class instance</param>
        /// <returns>The DELETE query string</returns>
        private static string BuildDeleteQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Where(p => p.Name.ToLower() == "id").Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            var query = string.Format("DELETE FROM {0} WHERE Id=@Id", tableName, string.Join(",", parameters));
            
            return query;
        }

        /// <summary>
        /// Generic synchronous Read extension method
        /// </summary>        
        /// <param name="id">Unique id</param>        
        /// <returns>The item corresponding to the given id if exists</returns>
        public static TEntity Get<TEntity, TId>(this IDbConnection connection, TId id)
            => connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id");

        /// <summary>
        /// Generic asynchronous Read extension method
        /// </summary>        
        /// <param name="id">Unique id</param>        
        /// <returns>The item corresponding to the given id if exists</returns>
        public static async ValueTask<TEntity> GetAsync<TEntity, TId>(this IDbConnection connection, TId id, CancellationToken cancellationToken)
            => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id", new { Id = id });

        /// <summary>
        /// Generic synchronous Read extension method
        /// </summary>                     
        /// <returns>all the table items</returns>
        public static IEnumerable<TEntity> GetAll<TEntity>(this IDbConnection connection)
            => connection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name}");

        /// <summary>
        /// Generic asynchronous Read extension method
        /// </summary>                     
        /// <returns>all the table items</returns>
        public static async ValueTask<IEnumerable<TEntity>> GetAllAsync<TEntity>(this IDbConnection connection, CancellationToken cancellationToken)
            => await connection.QueryAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name}");

        /// <summary>
        /// Generic row creation extension method
        /// </summary>        
        /// <param name="entity">the item to create</param>
        public static void Insert<TEntity>(this IDbConnection connection, dynamic entity)
            => SqlMapper.Query<TEntity>(connection, BuildInsertQuery<TEntity>(entity), entity);

        // <summary>
        /// Generic row creation extension method
        /// </summary>        
        /// <param name="entity">the item to create</param>
        public static async ValueTask InsertAsync<TEntity>(this IDbConnection connection, dynamic entity)
            => await SqlMapper.QueryAsync<TEntity>(connection, BuildInsertQuery<TEntity>(entity), entity);

        // <summary>
        /// Generic row update extension method
        /// </summary>     
        public static void Update<TEntity>(this IDbConnection connection, dynamic entity)
            => SqlMapper.Execute(connection, BuildUpdateQuery<TEntity>(entity), entity);

        // <summary>
        /// Generic row update extension method
        /// </summary>     
        public static async ValueTask UpdateAsync<TEntity>(this IDbConnection connection, dynamic entity, CancellationToken cancellationToken)
            => await SqlMapper.ExecuteAsync(connection, BuildUpdateQuery<TEntity>(entity), entity);

        // <summary>
        /// Generic row delete extension method
        /// </summary>     
        public static void Delete<TEntity>(this IDbConnection connection, dynamic entity)
            => SqlMapper.Execute(connection, BuildDeleteQuery<TEntity>(entity), entity);

        // <summary>
        /// Generic row count extension method
        /// </summary>     
        public static int Count<TEntity>(this IDbConnection connection)
            => connection.QueryFirst<int>($"SELECT COUNT(*) FROM {typeof(TEntity).Name}");

        // <summary>
        /// Generic search extension method
        /// </summary>     
        public static IEnumerable<TEntity> Find<TEntity>(this IDbConnection connection, Func<TEntity, bool> predicate)
            => connection.GetAll<TEntity>().Where(predicate);
    }
}
