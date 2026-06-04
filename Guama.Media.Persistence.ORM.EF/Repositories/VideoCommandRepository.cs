using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class VideoCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Video, System.Guid>
{
    public VideoCommandRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.Video> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Thumbnail);
    }
}