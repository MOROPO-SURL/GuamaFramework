namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class PhoneContactItemCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.PhoneContactItem, System.Guid>
{
    public PhoneContactItemCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
