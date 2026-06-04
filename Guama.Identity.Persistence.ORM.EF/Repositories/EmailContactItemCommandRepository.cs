namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class EmailContactItemCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.EmailContactItem, System.Guid>
{
    public EmailContactItemCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
