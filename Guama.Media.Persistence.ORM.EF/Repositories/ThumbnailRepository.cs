namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class ThumbnailRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Thumbnail, System.Guid>
    {
        public ThumbnailRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
