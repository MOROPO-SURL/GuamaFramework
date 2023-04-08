using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Extensions;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract
{
    public abstract class AbstractTraceableDbContext : AbstractFlexDbContext, ITraceableDbContext
    {
        public AbstractTraceableDbContext(IEnumerable<IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        public virtual bool TraceChanges { get => true; }

        public DbSet<Trace> Traces { get; set; }

        public int NativeSaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            return SaveChanges("unknow");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges("unknow", acceptAllChangesOnSuccess);
        }

        public virtual int SaveChanges(string author, bool acceptAllChangesOnSuccess = true)
        {
            return this.SaveTraceableChanges(base.SaveChanges, OnEntryChangesDetected, author, acceptAllChangesOnSuccess);
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync("unknow", true, cancellationToken);
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, System.Threading.CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync("unknow", acceptAllChangesOnSuccess, cancellationToken);
        }

        public virtual System.Threading.Tasks.Task<int> SaveChangesAsync(string author, bool acceptAllChangesOnSuccess = true, System.Threading.CancellationToken cancellationToken = default)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                return SaveChanges(author, acceptAllChangesOnSuccess);
            }, cancellationToken);
        }

        public virtual void OnEntryChangesDetected(List<EntityEntry> relevantEntryChanges)
        {
        }
    }
}
