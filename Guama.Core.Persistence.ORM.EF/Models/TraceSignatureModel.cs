using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Models
{
    internal class TraceSignatureModel
    {
        public long Id { get; set; }

        public Microsoft.EntityFrameworkCore.EntityState Type { get; set; }

        public string Author { get; set; }

        public System.DateTime Registered { get; private set; }

        public string EntityName { get; set; }

        public string BeforeData { get; set; }

        public string AfterData { get; set; }

        public long NextId { get; set; }

        public void Import(Trace trace)
        {
            Id = trace.Id;
            Type = trace.Type;
            Author = trace.Author;
            Registered = trace.Registered;
            EntityName = trace.EntityName;
            BeforeData = trace.BeforeData;
            AfterData = trace.AfterData;
            NextId = trace.NextId;
        }
    }
}
