using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Media.Persistence.ORM.EF.Context
{
    public interface IPersistenceDbContext
    {
        DbSet<Entities.Thumbnail> Thumbnails { get; set; }

        DbSet<Entities.Image> Images { get; set; }

        DbSet<Entities.Video> Videos { get; set; }
    }
}
