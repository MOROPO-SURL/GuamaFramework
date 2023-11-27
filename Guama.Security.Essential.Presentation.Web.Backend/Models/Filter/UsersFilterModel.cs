namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Models.Filter
{
    public class UsersFilterModel : Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<Persistence.Entities.User, string>
    {
        public string Id { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.User, bool>> GetSelector()
        {
            if (Id == null || Id == string.Empty) return null;

            return user => user.Id.Contains(Id);
        }
    }
}