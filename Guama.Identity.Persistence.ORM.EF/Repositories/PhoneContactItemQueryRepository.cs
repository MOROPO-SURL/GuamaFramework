namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PhoneContactItemQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.PhoneContactItem, System.Guid>
{
    public PhoneContactItemQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
