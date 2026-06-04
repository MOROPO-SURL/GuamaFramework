using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PostalContactItemQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.PostalContactItem, System.Guid>
{
    public PostalContactItemQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.PostalContactItem> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(entity => entity.Location);
    }
}
