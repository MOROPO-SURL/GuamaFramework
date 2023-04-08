namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class StateDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.State, System.Guid>
    {
        public StateDisplayViewModel()
        {
            Region = new RegionDisplayViewModel();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public RegionDisplayViewModel Region { get; set; }

        public void Import(Persistence.Entities.State entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            Region.Import(entity.Region);
        }
    }
}
