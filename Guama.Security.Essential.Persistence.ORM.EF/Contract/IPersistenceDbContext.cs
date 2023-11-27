using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Security.Essential.Persistence.ORM.EF.Contract
{
    public interface IPersistenceDbContext
    {
        DbSet<Entities.User> Users { get; set; }

        DbSet<Entities.UserRoleBridge> UserRoleBridges { get; set; }
    }
}
