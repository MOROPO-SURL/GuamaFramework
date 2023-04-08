namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Models.View
{
    public class MunicipalityDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Municipality, System.Guid>
    {
        public MunicipalityDisplayViewModel()
        {
            State = new StateDisplayViewModel();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public StateDisplayViewModel State { get; set; }

        public void Import(Persistence.Entities.Municipality entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            State.Import(entity.State);
        }
    }
}
