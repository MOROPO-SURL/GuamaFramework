using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class DocumentRepository : AbstractEntityRepository<Entities.Document, System.Guid>
    {
        public DocumentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Document> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.Thumbnail);
        }
    }
}
