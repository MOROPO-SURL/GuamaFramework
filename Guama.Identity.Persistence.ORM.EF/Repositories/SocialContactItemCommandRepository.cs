namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class SocialContactItemCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.SocialContactItem, System.Guid>
{
    public SocialContactItemCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
