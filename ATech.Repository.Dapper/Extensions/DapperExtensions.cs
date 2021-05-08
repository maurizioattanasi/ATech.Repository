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
    public static class DapperExtensions
    {
        private static string BuildInsertQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Where(p => p.Name.ToLower() != "id").Select(p => p.Name).ToArray();

            return string.Format("INSERT INTO {0} ({1}) OUTPUT inserted.ID VALUES (@{2})",
                                             tableName,
                                             string.Join(",", columns),
                                             string.Join(",@", columns));
        }

        private static string BuildUpdateQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            return string.Format("UPDATE {0} SET {1} WHERE Id=@Id", tableName, string.Join(",", parameters));
        }

        private static string BuildDeleteQuery<TEntity>(dynamic entity)
        {
            var tableName = typeof(TEntity).Name;

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string[] columns = propertyInfos.Where(p => p.Name.ToLower() == "id").Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            return string.Format("DELETE FROM {0} WHERE Id=@Id", tableName, string.Join(",", parameters));
        }

        public static TEntity Get<TEntity>(this IDbConnection connection, int id)
            => connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id");

        public static async Task<TEntity> GetAsync<TEntity>(this IDbConnection connection, int id, CancellationToken cancellationToken)
            => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id", new { Id = id });

        public static TEntity Get<TEntity>(this IDbConnection connection, Guid id)
            => connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id");

        public static async Task<TEntity> GetAsync<TEntity>(this IDbConnection connection, Guid id, CancellationToken cancellationToken)
            => await connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id=@Id", new { Id = id });

        public static IEnumerable<TEntity> GetAll<TEntity>(this IDbConnection connection)
            => connection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name}");

        public static async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(this IDbConnection connection, CancellationToken cancellationToken) 
            => await connection.QueryAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name}");
        public static TEntity Insert<TEntity>(this IDbConnection connection, dynamic entity)
        {
            IEnumerable<TEntity> results = SqlMapper.Query<TEntity>(connection, BuildInsertQuery<TEntity>(entity), entity);
            return results.First();
        }

        public static async Task<TEntity> InsertAsync<TEntity>(this IDbConnection connection, dynamic entity)
        {
            IEnumerable<TEntity> results = await SqlMapper.QueryAsync<TEntity>(connection, BuildInsertQuery<TEntity>(entity), entity);
            return results.First();
        }

        public static void Update<TEntity>(this IDbConnection connection, dynamic entity)
        {
            SqlMapper.Execute(connection, BuildUpdateQuery<TEntity>(entity), entity);
        }

        public static async Task UpdateAsync<TEntity>(this IDbConnection connection, dynamic entity)
        {
            await SqlMapper.ExecuteAsync(connection, BuildUpdateQuery<TEntity>(entity), entity);
        }

        public static void Delete<TEntity>(this IDbConnection connection, dynamic entity)
        {
            SqlMapper.Execute(connection, BuildDeleteQuery<TEntity>(entity), entity);
        }

        public static int Count<TEntity>(this IDbConnection connection)
        {
            var tableName = typeof(TEntity).Name;
            return connection.QueryFirst<int>($"SELECT COUNT(*) FROM {tableName}");
        }
    }
}
