namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class SocialContactItemQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.SocialContactItem, System.Guid>
{
    public SocialContactItemQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
