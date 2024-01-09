using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class ThumbnailRepository : AbstractEntityRepository<Entities.Thumbnail, System.Guid>
    {
        public ThumbnailRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
