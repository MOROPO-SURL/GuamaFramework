using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Contract
{
    public interface IPersistenceDbContext : Identity.Persistence.ORM.EF.Context.IPersistenceDbContext
    {
        DbSet<Entities.User> Users { get; set; }

        DbSet<Entities.UserRoleBridge> UserRoleBridges { get; set; }
    }
}
