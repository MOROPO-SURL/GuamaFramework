using Microsoft.EntityFrameworkCore;

namespace MOROPO.Framework.Guama.Location.Persistence.ORM.EF.Context
{
    public interface IPersistenceDbContext
    {
        DbSet<Entities.Region> Regions { get; set; }

        DbSet<Entities.Country> Countries { get; set; }

        DbSet<Entities.State> States { get; set; }

        DbSet<Entities.Municipality> Municipalities { get; set; }

        DbSet<Entities.Locality> Localities { get; set; }

        DbSet<Entities.Location> Locations { get; set; }

        DbSet<Entities.StateLocation> StateLocations { get; set; }

        DbSet<Entities.MunicipalityLocation> MunicipalityLocations { get; set; }

        DbSet<Entities.LocalityLocation> LocalityLocations { get; set; }
    }
}
