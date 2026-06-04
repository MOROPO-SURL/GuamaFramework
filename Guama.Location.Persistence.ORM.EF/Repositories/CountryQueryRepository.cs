namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class CountryQueryRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractQueryEntityRepository<Entities.Country, System.Guid>
{
    public CountryQueryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}