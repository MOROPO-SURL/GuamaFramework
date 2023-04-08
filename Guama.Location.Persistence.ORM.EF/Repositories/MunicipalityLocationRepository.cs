using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class MunicipalityLocationRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.MunicipalityLocation, System.Guid>
    {
        public MunicipalityLocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.MunicipalityLocation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Municipality).ThenInclude(e => e.State).ThenInclude(e => e.Region).ThenInclude(e => e.Country);
        }
    }
}
