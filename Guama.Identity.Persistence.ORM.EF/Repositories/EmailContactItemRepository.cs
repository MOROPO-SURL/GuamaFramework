namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class EmailContactItemRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.EmailContactItem, System.Guid>
    {
        public EmailContactItemRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
