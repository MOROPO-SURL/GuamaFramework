using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories;

public class TraceQueryRepository : Contract.AbstractQueryEntityRepository<Trace, long>
{
    public TraceQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}
