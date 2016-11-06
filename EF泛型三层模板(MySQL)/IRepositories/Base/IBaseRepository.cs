using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IRepositories
{
    /// <summary>
    /// 约定所有表数据的通用增，删，查，改功能
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region 条件查询
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 条件链表查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="tableNames">表名</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, string[] tableNames);
        #endregion

        #region 分页查询
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="tcount">总条数</param>
        /// <param name="ascending">升序:true 降序:false</param>
        /// <returns></returns>
        IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, int pageIndex, int pageSize, out int pcount, bool ascending = true);

        /// <summary>
        /// 条件链表分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="tableNames">表名</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="tcount">总条数</param>
        /// <param name="ascending">升序:true 降序:false</param>
        /// <returns></returns>
        IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, string[] tableNames, int pageIndex, int pageSize, out int pcount, bool ascending = true);
        #endregion

        /// <summary>
        /// 执行SQL查询 可调用存储过程
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        List<TElement> ExecSql<TElement>(string sql, params object[] ps);

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        void Add(TEntity model);
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <param name="propertyNames">修改的属性名称和数组</param>
        void Edit(TEntity model, string[] propertyNames);
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">删除的实体</param>
        /// <param name="isAddDbContext">true：表示model已经追加到EF容器中</param>
        void Delete(TEntity model, bool isAddDbContext);
        #endregion

        #region 保存
        /// <summary>
        /// 统一保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 统一保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        #endregion
    }
}
