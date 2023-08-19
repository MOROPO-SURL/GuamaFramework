using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Repositories
{
    public class DocumentRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Document, System.Guid>
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
