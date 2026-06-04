namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories;

public class WebSiteContactItemQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.WebSiteContactItem, System.Guid>
{
    public WebSiteContactItemQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
