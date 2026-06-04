using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class DocumentCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Document, System.Guid>
{
    public DocumentCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<Entities.Document> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Thumbnail);
    }
}
