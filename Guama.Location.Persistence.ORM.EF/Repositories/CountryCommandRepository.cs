namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Repositories;

public class CountryCommandRepository : Core.Persistence.ORM.EF.Repositories.Contract.AbstractCommandEntityRepository<Entities.Country, System.Guid>
{
    public CountryCommandRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}