using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class MunicipalityRepository : AbstractEntityRepository<Entities.Municipality, System.Guid>
    {
        public MunicipalityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Municipality, object> GetPagingOrderSelector()
        {
            return e => e.Name;
        }

        protected override System.Linq.IQueryable<Entities.Municipality> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}