using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IRepositories;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public EntityFrameworkDbContext db
        {
            get
            {
                //判断当前子线程的缓存中是否有ef容器对象，如果没有则创建存入子线程的缓存
                object dataObj = CallContext.GetData("ApplicationDbContext");
                if (dataObj == null)
                {
                    dataObj = new EntityFrameworkDbContext();

                    CallContext.SetData("ApplicationDbContext", dataObj);
                }

                return dataObj as EntityFrameworkDbContext;
            }
        }

        DbSet<TEntity> _dbSet;

        public BaseRepository()
        {
            _dbSet = db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            DbQuery<TEntity> query = _dbSet;

            foreach (var tableName in tableNames)
            {
                query = query.Include(tableName);
            }

            return query.Where(where);
        }

        public virtual IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, int pageIndex, int pageSize, out int pcount, bool ascending = true)
        {
            //计算出要跳过的数据总条数
            int skipCount = (pageIndex - 1) * pageSize;

            ///计算符合条件的总条数
            pcount = _dbSet.Count(where);

            //返回分页数据
            if (ascending)
            {
                return _dbSet.Where(where).OrderBy(orderby).Skip(skipCount).Take(pageSize);
            }
            else
            {
                return _dbSet.Where(where).OrderByDescending(orderby).Skip(skipCount).Take(pageSize);
            }
        }

        public virtual IQueryable<TEntity> PageList<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderby, string[] tableNames, int pageIndex, int pageSize, out int pcount, bool ascending = true)
        {
            DbQuery<TEntity> query = _dbSet;
            foreach (var tableName in tableNames)
            {
                query = query.Include(tableName);
            }

            //计算出要跳过的数据总条数
            int skipCount = (pageIndex - 1) * pageSize;

            ///计算符合条件的总条数
            pcount = _dbSet.Count(where);

            //返回分页数据
            if (ascending)
            {
                return _dbSet.Where(where).OrderBy(orderby).Skip(skipCount).Take(pageSize);
            }
            else
            {
                return _dbSet.Where(where).OrderByDescending(orderby).Skip(skipCount).Take(pageSize);
            }

        }

        public virtual int SaveChanges()
        {
            return db.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        public virtual List<TElement> ExecSql<TElement>(string sql, params object[] ps)
        {
            return db.Database.SqlQuery<TElement>(sql, ps).ToList();
        }

        public virtual void Add(TEntity model)
        {
            _dbSet.Add(model);
        }

        public virtual void Delete(TEntity model, bool isAddDbContext)
        {
            //表示没有追加到EF容器就追加
            if (isAddDbContext == false)
            {
                _dbSet.Attach(model);
            }

            //将已经追加到了EF容器中的代理类状态修改为Deleted状态
            _dbSet.Remove(model);

        }

        public virtual void Edit(TEntity model, string[] propertyNames)
        {
            var entry = db.Entry(model);
            entry.State = EntityState.Unchanged;

            foreach (var item in propertyNames)
            {
                entry.Property(item).IsModified = true;
            }
        }
    }
}
