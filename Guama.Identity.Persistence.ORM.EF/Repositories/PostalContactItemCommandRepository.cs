using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PostalContactItemCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.PostalContactItem, System.Guid>
{
    public PostalContactItemCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.PostalContactItem> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(entity => entity.Location);
    }
}
