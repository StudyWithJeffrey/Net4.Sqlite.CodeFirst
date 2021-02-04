using LY.ExamMonitor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LY.ExamMonitor.Core.Interfaces
{
    /// <summary>
    /// 数据仓库接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {

        /// <summary>
        /// 获取指定Id的数据
        /// </summary> 
        /// <param name="where"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// 获取查询条件的数据
        /// </summary> 
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取查询条件的总条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> where);

        /// <summary>
        /// 分页查询
        /// </summary> 
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">总数量</param>
        /// <returns></returns>
        List<T> GetPage<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total);

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
        List<TResult> GetPage<TResult, TKey>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int total);

        /// <summary>
        /// 插入数据
        /// </summary> 
        /// <param name="t"></param>
        /// <returns></returns>
        T Add(T t);

        /// <summary>
        /// 修改数据
        /// </summary> 
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(T t);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="t"></param> 
        /// <returns></returns>
        int Delete(T t);

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
