namespace MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Backend.Models.View
{
    public class RoleDisplayViewModel : Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Role, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.Role entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
