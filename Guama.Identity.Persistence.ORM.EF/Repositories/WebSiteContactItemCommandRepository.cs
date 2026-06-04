namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class WebSiteContactItemCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.WebSiteContactItem, System.Guid>
{
    public WebSiteContactItemCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
