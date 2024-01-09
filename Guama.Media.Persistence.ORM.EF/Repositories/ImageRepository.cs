using System.Linq;
using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class ImageRepository : AbstractEntityRepository<Entities.Image, System.Guid>
    {
        public ImageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Image> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Thumbnail);
        }
    }
}
