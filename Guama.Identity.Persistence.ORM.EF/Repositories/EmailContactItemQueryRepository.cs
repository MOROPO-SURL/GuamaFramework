namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class EmailContactItemQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.EmailContactItem, System.Guid>
{
    public EmailContactItemQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
