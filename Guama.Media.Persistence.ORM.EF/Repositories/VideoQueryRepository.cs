using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class VideoQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Video, System.Guid>
{
    public VideoQueryRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Entities.Video> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(e => e.Thumbnail);
    }
}