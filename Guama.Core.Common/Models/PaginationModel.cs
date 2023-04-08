using System.Collections.Generic;

namespace MOROPO.Framework.Guama.Core.Presentation.Models
{
    public class PaginationModel<TModel>
    {
        public List<TModel> List { get; set; }

        public int Page { get; set; }

        public bool HasNext { get; set; }

        public bool HasPrevious { get; set; }

        public int MaxPageCount { get; set; }

        public int Limit { get; set; }
    }
}
