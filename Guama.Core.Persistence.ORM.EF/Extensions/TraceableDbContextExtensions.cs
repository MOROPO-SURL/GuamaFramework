using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Context.Contract;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Extensions
{
    public static class TraceableDbContextExtensions
    {
        public static int SaveTraceableChanges(this ITraceableDbContext target, System.Func<bool, int> baseSaveChangesFunc, System.Action<List<EntityEntry>> onEntryChangesDetectedAction, string author, bool acceptAllChangesOnSuccess)
        {
            var dbContextTarget = (DbContext)target;

            var result = 0;

            var relevantEntryChanges = dbContextTarget.ChangeTracker.Entries().Where(changeEntry => !(changeEntry.Entity is Trace) && (changeEntry.State == EntityState.Added || changeEntry.State == EntityState.Deleted || changeEntry.State == EntityState.Modified)).ToList();

            var changesTypes = new List<EntityState>();
            var beforeDatas = new List<string>();

            foreach (var changeEntry in relevantEntryChanges)
            {
                changesTypes.Add(changeEntry.State);
                beforeDatas.Add(Newtonsoft.Json.JsonConvert.SerializeObject(changeEntry.OriginalValues.ToObject()));
            };

            onEntryChangesDetectedAction.Invoke(relevantEntryChanges);

            result += baseSaveChangesFunc.Invoke(acceptAllChangesOnSuccess);

            if (!target.TraceChanges) return result;

            var counter = 0;

            foreach (var changeEntry in relevantEntryChanges)
            {
                var trace = new Trace
                {
                    Type = changesTypes[counter],
                    Author = author,
                    EntityName = changeEntry.Metadata.Name,
                    BeforeData = beforeDatas[counter],
                    AfterData = Newtonsoft.Json.JsonConvert.SerializeObject(changeEntry.CurrentValues.ToObject())
                };

                Trace lastTrace = null;

                if (target.Traces.Any())
                {
                    lastTrace = target.Traces.Last();
                }

                target.Traces.Add(trace);

                result += baseSaveChangesFunc.Invoke(acceptAllChangesOnSuccess);

                trace.Signature = Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(trace);

                target.Traces.Update(trace);

                result += baseSaveChangesFunc.Invoke(acceptAllChangesOnSuccess);

                if (lastTrace != null)
                {
                    lastTrace.NextId = trace.Id;

                    lastTrace.Signature = Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(lastTrace);

                    target.Traces.Update(lastTrace);

                    result += baseSaveChangesFunc.Invoke(acceptAllChangesOnSuccess);
                }

                counter++;
            }

            return result + baseSaveChangesFunc.Invoke(acceptAllChangesOnSuccess);
        }
    }
}
