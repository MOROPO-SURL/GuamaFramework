using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Filters;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes
{
    public class ChangeUserPasswordRequiredActionAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable { get { return false; } }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<ChangeUserPasswordRequiredActionFilter>();
        }
    }
}
