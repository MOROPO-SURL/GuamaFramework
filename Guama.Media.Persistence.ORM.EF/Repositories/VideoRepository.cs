using System.Linq;
using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class VideoRepository : AbstractEntityRepository<Entities.Video, System.Guid>
    {
        public VideoRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Video> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Thumbnail);
        }
    }
}
