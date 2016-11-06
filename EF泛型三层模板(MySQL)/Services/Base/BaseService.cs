using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        //靠注入实例化
        protected IBaseRepository<TEntity> baseDal;

        public virtual void Add(TEntity model)
        {
            baseDal.Add(model);
        }

        public virtual void Delete(TEntity model, bool isAddDbContext)
        {
            baseDal.Delete(model, isAddDbContext);
        }

        public virtual void Edit(TEntity model, string[] propertyNames)
        {
            baseDal.Edit(model, propertyNames);
        }

        public virtual List<TElement> ExecSql<TElement>(string sql, params object[] ps)
        {
            return baseDal.ExecSql<TElement>(sql, ps);
        }

        public virtual IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, int pageIndex, int pageSize, out int pcount, bool ascending = true)
        {
            return baseDal.PageList<TKey>(where, orderby, pageIndex, pageSize, out pcount, ascending);
        }

        public virtual IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, string[] tableNames, int pageIndex, int pageSize, out int pcount, bool ascending = true)
        {
            return baseDal.PageList(where, orderby, pageIndex, pageSize, out pcount, ascending);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return baseDal.Query(where);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return baseDal.Query(where, tableNames);
        }

        public virtual int SaveChanges()
        {
            return baseDal.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return baseDal.SaveChangesAsync();
        }
    }
}
