using LY.ExamMonitor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;

namespace LY.ExamMonitor.Data
{
    public abstract class SqliteDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {

        private readonly int _currentSchemaVersion;

        public SqliteDbContext() { }

        protected SqliteDbContext(string connectionString, int currentSchemaVersion)
            : base(new SQLiteConnection(connectionString), true)
        {
            _currentSchemaVersion = currentSchemaVersion;
        }

        //protected SqliteDbContext(string nameOrConnectionString):base(nameOrConnectionString)
        //{

        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new CreateOrMigrateDBInitializer<TDbContext>(modelBuilder, _currentSchemaVersion));


            modelBuilder.Configurations.AddFromAssembly(typeof(TDbContext).Assembly);

        }


        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public DbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        #region Query

        /// <summary>
        /// DbSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbSet<T> Query<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        /// <summary>
        /// DbQuery
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbQuery<T> QueryAsNoTracking<T>() where T : BaseEntity
        {
            return this.Query<T>().AsNoTracking();
        }

        /// <summary>
        /// IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Queryable<T>() where T : BaseEntity
        {
            return this.Query<T>().AsQueryable();
        }

        /// <summary>
        /// 获取指定Id的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseEntity
        {
            return this.Find<T>(id);
        }

        /// <summary>
        /// 获取查询条件的总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int QueryCount<T>(Expression<Func<T, bool>> where) where T : BaseEntity
        {
            return this.Query<T>().AsNoTracking().Where(where).Count();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">查询表类型</typeparam> 
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        public List<T> QueryPage<T, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total) where T : BaseEntity
        {
            total = QueryCount<T>(where);

            return this.Query<T>().AsNoTracking()
                       .Where(where)
                       .OrderByDescending(order)
                       .Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">查询表类型</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="selector">查询对象</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        public List<TResult> QueryPage<T, TResult, TKey>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total)
            where T : BaseEntity
        {
            total = QueryCount<T>(where);

            var list = this.Query<T>().AsNoTracking()
                           .Where(where)
                           .OrderByDescending(order)
                           .Select<T, TResult>(selector)
                           .Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize);
            return list.ToList();
        }

        /// <summary>
        /// SQL语句查询列表
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="sqlScript"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<V> SqlQuery<V>(string sqlScript, params SQLiteParameter[] parameters) where V : BaseEntity
        {
            return this.Database
                       .SqlQuery<V>(sqlScript, parameters)
                       .ToList();
        }

        /// <summary>
        /// SQL语句查询单条记录或单个值
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="sqlScript"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public V SqlQueryScalar<V>(string sqlScript, params SQLiteParameter[] parameters)
        {
            return this.Database
                       .SqlQuery<V>(sqlScript, parameters)
                       .SingleOrDefault();
        }

        /// <summary>
        /// SQL执行返回影响的行数
        /// </summary>
        /// <param name="sqlScript"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sqlScript, params SQLiteParameter[] parameters)
        {
            return this.Database
                       .ExecuteSqlCommand(sqlScript, parameters);
        }

        #endregion

        #region Save

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : BaseEntity
        {
            return base.Set<T>().Add(t);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Delete<T>(T t) where T : BaseEntity
        {
            return base.Set<T>().Remove(t);
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        #endregion

        /// <summary>
        /// 释放时，将读写锁释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            finally
            {

            }
        }
    }
}
