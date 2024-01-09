using System.Linq;
using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Identity.Persistence.ORM.EF.Repositories
{
    public class PostalContactItemRepository : AbstractEntityRepository<Entities.PostalContactItem, System.Guid>
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
