using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class ImageCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Image, System.Guid>
{
    public ImageCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.Image> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(entity => entity.Thumbnail);
    }
}
