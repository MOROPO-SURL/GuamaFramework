namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class LocalityDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Locality, System.Guid>
    {
        public LocalityDisplayViewModel()
        {
            Municipality = new MunicipalityDisplayViewModel();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public MunicipalityDisplayViewModel Municipality { get; set; }

        public void Import(Persistence.Entities.Locality entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            Municipality.Import(entity.Municipality);
        }
    }
}
