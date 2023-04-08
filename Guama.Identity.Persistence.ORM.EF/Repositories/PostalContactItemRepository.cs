using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class PostalContactItemRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.PostalContactItem, System.Guid>
    {
        public PostalContactItemRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.PostalContactItem> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Location);
        }
    }
}
