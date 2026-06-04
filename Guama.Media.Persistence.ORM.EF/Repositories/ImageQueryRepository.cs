using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class ImageQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Image, System.Guid>
{
    public ImageQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.Image> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(entity => entity.Thumbnail);
    }
}
