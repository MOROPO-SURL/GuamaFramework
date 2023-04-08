using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories
{
    public abstract class AbstractEntityRepository<TEntity, TId> : Persistence.Repositories.IEntityRepository<TEntity, TId>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
    {
        protected readonly DbContext DbContext;

        public AbstractEntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual IQueryable<TEntity> GetQueryableDbSet()
        {
            return DbContext.Set<TEntity>();
        }

        protected virtual IQueryable<TSpecializationEntity> GetQueryableDbSet<TSpecializationEntity>()
        {
            return DbContext.Set<TEntity>().Cast<TSpecializationEntity>();
        }

        public TEntity Get(TId id)
        {
            return GetQueryableDbSet().First(GetKeySelector(id));
        }

        public TEntity FindOneBy(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQueryableDbSet().FirstOrDefault(criteria);
        }

        public TSpecializationEntity FindOneBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
        {
            return GetQueryableDbSet<TSpecializationEntity>().FirstOrDefault(criteria);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null) return GetQueryableDbSet();

            return GetQueryableDbSet().Where(criteria);
        }

        public IQueryable<TSpecializationEntity> FindBy<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
        {
            if (criteria == null) return GetQueryableDbSet<TSpecializationEntity>();

            return GetQueryableDbSet<TSpecializationEntity>().Where(criteria);
        }

        public IQueryable<TEntity> GetAll()
        {
            return GetQueryableDbSet();
        }

        public IQueryable<TSpecializationEntity> GetAll<TSpecializationEntity>() where TSpecializationEntity : class, TEntity
        {
            return GetQueryableDbSet<TSpecializationEntity>();
        }

        public List<TEntity> GetPage(int page, int limit)
        {
            var skip = page * limit;

            if (!IsPagingOrderDescending())
            {
                return GetQueryableDbSet().OrderBy(GetPagingOrderSelector()).Skip(skip).Take(limit).ToList();
            }
            else
            {
                return GetQueryableDbSet().OrderByDescending(GetPagingOrderSelector()).Skip(skip).Take(limit).ToList();
            }
        }

        public virtual TEntity Add(TEntity entity, string author, bool commit = true)
        {
            DbContext.Set<TEntity>().Add(entity);

            if (commit)
            {
                if (DbContext is ITraceableDbContext)
                {
                    (DbContext as ITraceableDbContext).SaveChanges(author);
                }
                else
                {
                    DbContext.SaveChanges();
                }
            }

            return entity;
        }

        public virtual TEntity Update(TEntity entity, string author, bool commit = true)
        {
            var entry = DbContext.Entry(entity);

            entry.State = EntityState.Modified;

            if (commit)
            {
                if (DbContext is ITraceableDbContext)
                {
                    (DbContext as ITraceableDbContext).SaveChanges(author);
                }
                else
                {
                    DbContext.SaveChanges();
                }
            }

            return entity;
        }

        public virtual void Delete(TEntity entity, string author, bool commit = true)
        {
            DbContext.Set<TEntity>().Remove(entity);

            if (commit)
            {
                if (DbContext is ITraceableDbContext)
                {
                    (DbContext as ITraceableDbContext).SaveChanges(author);
                }
                else
                {
                    DbContext.SaveChanges();
                }
            }
        }

        public int Count()
        {
            return DbContext.Set<TEntity>().Count();
        }

        public int Count<TSpecializationEntity>() where TSpecializationEntity : class, TEntity
        {
            return DbContext.Set<TSpecializationEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return DbContext.Set<TEntity>().Count(criteria);
        }

        public int Count<TSpecializationEntity>(Expression<Func<TSpecializationEntity, bool>> criteria) where TSpecializationEntity : class, TEntity
        {
            return DbContext.Set<TSpecializationEntity>().Count(criteria);
        }

        protected virtual Func<TEntity, bool> GetKeySelector(TId key)
        {
            return e => e.Id.Equals(key);
        }

        public virtual Func<TEntity, object> GetPagingOrderSelector()
        {
            return e => e.Id;
        }

        public virtual bool IsPagingOrderDescending()
        {
            return false;
        }
    }
}
