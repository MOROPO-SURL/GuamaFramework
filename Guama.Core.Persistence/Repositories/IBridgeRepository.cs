using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;

namespace MOROPO.Framework.Guama.Core.Persistence.Repositories
{
    public interface IBridgeRepository<TBridge, TProwEntity, TProwId, TSternEntity, TSternId> 
        where TBridge: class, IBridge<TProwEntity, TProwId, TSternEntity, TSternId>
        where TProwEntity : class, IEntity<TProwId>
        where TSternEntity : class, IEntity<TSternId>
    {
        TBridge Get(TProwId prowId, TSternId sternId);

        TBridge FindOneBy(Expression<Func<TBridge, bool>> criteria);

        IQueryable<TBridge> FindBy(Expression<Func<TBridge, bool>> criteria);

        IQueryable<TBridge> GetAll();

        List<TBridge> GetPage(int page, int limit);

        TBridge Add(TBridge bridge, string author, bool commit = true);

        TBridge Update(TBridge entity, string author, bool commit = true);

        void Delete(TBridge bridge, string author, bool commit = true);

        int Count();

        int Count(Expression<Func<TBridge, bool>> criteria);

        Func<TBridge, object> GetPagingOrderSelector();

        bool IsPagingOrderDescending();
    }
}
