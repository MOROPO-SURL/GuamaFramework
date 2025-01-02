using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class CountryRepository : AbstractEntityRepository<Entities.Country, System.Guid>
    {
        public CountryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
