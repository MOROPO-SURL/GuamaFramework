using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories;

public class TraceCommandRepository : Contract.AbstractQueryEntityRepository<Trace, long>
{
    public TraceCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}