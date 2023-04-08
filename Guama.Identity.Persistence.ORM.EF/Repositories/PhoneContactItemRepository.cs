namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class PhoneContactItemRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.PhoneContactItem, System.Guid>
    {
        public PhoneContactItemRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
