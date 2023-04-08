namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories
{
    public class CountryRepository : Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Country, System.Guid>
    {
        public CountryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Country, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
