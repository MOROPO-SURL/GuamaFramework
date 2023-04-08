using Microsoft.AspNetCore.Localization;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.Localization
{
    public class UrlRequestCultureProvider : IRequestCultureProvider
    {
        public System.Threading.Tasks.Task<ProviderCultureResult> DetermineProviderCultureResult(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var url = httpContext.Request.Path;

            var parts = httpContext.Request.Path.Value.Split('/').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            if (parts.Count() <= 0)
            {
                return System.Threading.Tasks.Task.FromResult<ProviderCultureResult>(null);
            }

            var targetPart = parts.First();

            if (!System.Text.RegularExpressions.Regex.IsMatch(targetPart, @"^[a-z]{2}(?:-[A-Z]{2})?$"))
            {
                return System.Threading.Tasks.Task.FromResult<ProviderCultureResult>(null);
            }

            return System.Threading.Tasks.Task.FromResult(new ProviderCultureResult(targetPart));
        }
    }
}
