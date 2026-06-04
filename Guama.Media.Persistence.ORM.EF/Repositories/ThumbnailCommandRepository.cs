namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories;

public class ThumbnailCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Thumbnail, System.Guid>
{
    public ThumbnailCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
