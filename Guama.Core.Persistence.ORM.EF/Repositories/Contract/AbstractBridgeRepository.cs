using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract
{
    public abstract class AbstractBridgeRepository<TBridge, TProwEntity, TProwId, TSternEntity, TSternId> : Persistence.Repositories.IBridgeRepository<TBridge, TProwEntity, TProwId, TSternEntity, TSternId>
        where TBridge : class, IBridge<TProwEntity, TProwId, TSternEntity, TSternId>
        where TProwEntity : class, IEntity<TProwId>
        where TSternEntity : class, IEntity<TSternId>
    {
        protected readonly DbContext DbContext;

        public AbstractBridgeRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual IQueryable<TBridge> GetQueryableDbSet()
        {
            return DbContext.Set<TBridge>();
        }

        protected virtual IQueryable<TSpecializationEntity> GetQueryableDbSet<TSpecializationEntity>()
        {
            return DbContext.Set<TBridge>().Cast<TSpecializationEntity>();
        }

        public TBridge Get(TProwId prowId, TSternId sternId)
        {
            return GetQueryableDbSet().First(GetKeySelector(prowId, sternId));
        }

        public TBridge FindOneBy(System.Linq.Expressions.Expression<System.Func<TBridge, bool>> criteria)
        {
            return GetQueryableDbSet().FirstOrDefault(criteria);
        }

        public IQueryable<TBridge> FindBy(System.Linq.Expressions.Expression<System.Func<TBridge, bool>> criteria)
        {
            if (criteria == null) return GetQueryableDbSet();

            return GetQueryableDbSet().Where(criteria);
        }

        public IQueryable<TBridge> GetAll()
        {
            return GetQueryableDbSet();
        }

        public System.Collections.Generic.List<TBridge> GetPage(int page, int limit)
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

        public virtual TBridge Add(TBridge bridge, string author, bool commit = true)
        {
            DbContext.Set<TBridge>().Add(bridge);

            if (commit)
            {
                if (DbContext is Context.Contract.ITraceableDbContext)
                {
                    (DbContext as Context.Contract.ITraceableDbContext).SaveChanges(author, commit);
                }
                else
                {
                    DbContext.SaveChanges(commit);
                }
            }

            return bridge;
        }

        public virtual void Delete(TBridge bridge, string author, bool commit = true)
        {
            DbContext.Set<TBridge>().Remove(bridge);

            if (commit)
            {
                if (DbContext is Context.Contract.ITraceableDbContext)
                {
                    (DbContext as Context.Contract.ITraceableDbContext).SaveChanges(author, commit);
                }
                else
                {
                    DbContext.SaveChanges(commit);
                }
            }
        }

        public int Count()
        {
            return DbContext.Set<TBridge>().Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<TBridge, bool>> criteria)
        {
            return DbContext.Set<TBridge>().Count(criteria);
        }

        protected virtual System.Func<TBridge, bool> GetKeySelector(TProwId prowId, TSternId sternId)
        {
            return bridge => bridge.ProwId.Equals(prowId) && bridge.SternId.Equals(sternId);
        }

        public virtual System.Func<TBridge, object> GetPagingOrderSelector()
        {
            return bridge => bridge.SternId;
        }

        public virtual bool IsPagingOrderDescending()
        {
            return false;
        }
    }
}
