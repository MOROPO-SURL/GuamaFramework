namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class SocialContactItemRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.SocialContactItem, System.Guid>
    {
        public SocialContactItemRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
