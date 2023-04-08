using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract
{
    public interface ITraceableDbContext
    {
        bool TraceChanges { get; }

        Microsoft.EntityFrameworkCore.DbSet<Trace> Traces { get; set; }

        int SaveChanges(string author, bool acceptAllChangesOnSuccess = true);

        System.Threading.Tasks.Task<int> SaveChangesAsync(string author, bool acceptAllChangesOnSuccess = true, System.Threading.CancellationToken cancellationToken = default);

        int NativeSaveChanges(bool acceptAllChangesOnSuccess = true);
    }
}
