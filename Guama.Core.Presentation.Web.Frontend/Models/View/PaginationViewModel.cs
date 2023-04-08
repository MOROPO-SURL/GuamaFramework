using System.Collections.Generic;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Frontend.Models.View
{
    public class PaginationViewModel<TEntity, TId, TDisplayViewModel> : Presentation.Models.PaginationModel<TDisplayViewModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayViewModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>
    {
        public new List<TDisplayViewModel> List { get; set; }

        public string Action { get; set; }

        public object Filter { get; set; }

        public IDictionary<string, string> RouteValues { get; set; }
    }
}
