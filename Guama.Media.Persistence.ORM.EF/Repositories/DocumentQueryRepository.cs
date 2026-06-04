using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class DocumentQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Document, System.Guid>
{
    public DocumentQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Document> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Thumbnail);
    }
}
