using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BrighterCapAPI.Helpers
{
    public static class ContextExtensions
    {
        public static async Task<bool> AddOrUpdate<T>(this DbSet<T> ctx, T entity, Expression<Func<T, bool>> keyCheck, bool neverUpdate = false) where T : class
        {
            var found = await ctx.AnyAsync(keyCheck);

            if (found)
            {
                if (!neverUpdate)
                    ctx.Update(entity);
                return true;
            }
            else
            {
                await ctx.AddAsync(entity);
                return false;
            }
        }

        public static async Task<Tuple<int, int>> AddOrUpdateRange<T>(this DbSet<T> ctx, IEnumerable<T> entities, DbContext dbContext, Expression<Func<T, T, bool>> keyCheck, bool neverUpdate = false) where T : class
        {
            int created = 0, updated = 0;
            foreach (var entity in entities)
                if (await AddOrUpdate(ctx, entity, Bind2nd(keyCheck, entity), neverUpdate))
                    updated++;
                else
                    created++;

            await dbContext.SaveChangesAsync();

            return new Tuple<int, int>(created, updated);
        }
        public static Expression<Func<T1, TResult>> Bind2nd<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> source, T2 argument)
        {
            Expression arg2 = Expression.Constant(argument, typeof(T2));
            Expression newBody = new Rewriter(source.Parameters[1], arg2).Visit(source.Body);
            return Expression.Lambda<Func<T1, TResult>>(newBody, source.Parameters[0]);
        }

        public static async Task<IList<T>> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection());

            return await db2.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        public static async Task<int> SqlIntQuery(this DbContext _context, string sql)
        {
            using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sql;
            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public static async Task<IEnumerable<string>> SqlStringQuery(this DbContext _context, string sql)
        {
            using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = sql;
            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            var list = new List<string>();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                list.Add((string)reader[0]);

            return list;
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }

        #region Nested type: Rewriter

        private class Rewriter : ExpressionVisitor
        {
            private readonly Expression candidate_;
            private readonly Expression replacement_;

            public Rewriter(Expression candidate, Expression replacement)
            {
                candidate_ = candidate;
                replacement_ = replacement;
            }

            public override Expression Visit(Expression node)
            {
                return node == candidate_ ? replacement_ : base.Visit(node);
            }
        }

        #endregion
    }
}