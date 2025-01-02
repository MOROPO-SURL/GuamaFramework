using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories
{
    public class TraceRepository : AbstractEntityRepository<Trace, long>
    {
        public TraceRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
