using LY.ExamMonitor.Core.Entities;
using LY.ExamMonitor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LY.ExamMonitor.Data.Respositories
{
    /// <summary>
    /// 数据仓库基类
    /// </summary>
    /// <typeparam name="T">对应的实体类型</typeparam>
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly SqliteDbContext<ExamMonitorContext> _dbContext;

        public EfRepository(SqliteDbContext<ExamMonitorContext> dbContext)
        {
            _dbContext = dbContext;
        }

        #region Get

        /// <summary>
        /// 获取指定Id的数据
        /// </summary> 
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

        /// <summary>
        /// 获取查询条件的数据
        /// </summary> 
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> where)
        {
            return _dbContext.Query<T>().Where(where).ToList();
        }

        /// <summary>
        /// 获取查询条件的总条数
        /// </summary> 
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> where)
        {
            return _dbContext.QueryCount<T>(where);
        }

        /// <summary>
        /// 分页查询
        /// </summary> 
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        public List<T> GetPage<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total)
        { 
            return _dbContext.QueryPage<T, TKey>(where, order, pageIndex, pageSize, out total);
        }

        /// <summary>
        /// 分页查询
        /// </summary> 
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="selector">查询对象</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        public List<TResult> GetPage<TResult, TKey>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total)
        {
            return _dbContext.QueryPage<T, TResult, TKey>(selector, where, order, pageIndex, pageSize, out total);
        }

        #endregion

        #region Changes

        /// <summary>
        /// 插入数据
        /// </summary> 
        /// <param name="t"></param>
        /// <returns></returns>
        public T Add(T t)
        {
            var entity = _dbContext.Insert(t);

            if (SaveChanges() > 0)
            {
                return entity;
            }

            return null;
        }

        /// <summary>
        /// 修改数据
        /// </summary> 
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="t"></param> 
        /// <returns></returns>
        public int Delete(T t)
        {
            _dbContext.Delete<T>(t);
            return SaveChanges();
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        #endregion

    }
}
