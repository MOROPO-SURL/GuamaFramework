using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities
{
    public class Trace : Persistence.Entities.Contract.IEntity<long>, Persistence.Entities.Contract.IPersistenceSignable
    {
        [Key]
        public long Id { get; set; }

        public Microsoft.EntityFrameworkCore.EntityState Type { get; set; }

        public string Author { get; set; }

        [Required]
        public System.DateTime Registered { get; private set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public string BeforeData { get; set; }

        public string AfterData { get; set; }

        [Required]
        public long NextId { get; set; }

        public string Signature { get; set; }

        public Trace()
        {
            Registered = System.DateTime.Now;
            NextId = 0;
            Signature = string.Empty;
        }

        public object GetSignableEntityModel()
        {
            var model = new Models.TraceSignatureModel();

            model.Import(this);

            return model;
        }
    }
}
