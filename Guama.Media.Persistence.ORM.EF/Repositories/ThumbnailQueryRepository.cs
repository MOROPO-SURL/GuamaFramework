namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class ThumbnailQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Thumbnail, System.Guid>
{
    public ThumbnailQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
