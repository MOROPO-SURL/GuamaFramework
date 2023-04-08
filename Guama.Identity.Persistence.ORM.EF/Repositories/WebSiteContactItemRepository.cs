namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class WebSiteContactItemRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.WebSiteContactItem, System.Guid>
    {
        public WebSiteContactItemRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
