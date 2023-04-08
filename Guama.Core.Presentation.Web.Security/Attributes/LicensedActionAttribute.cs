using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Filters;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class LicensedActionAttribute : System.Attribute, IFilterFactory
    {
        public bool IsReusable { get { return false; } }

        public IFilterMetadata CreateInstance(System.IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<LicensedActionFilter>();
        }
    }
}
