using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.ModelBinding
{
    public sealed class AbstractsModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsCollectionType &&
                (context.Metadata.ModelType.GetTypeInfo().IsInterface ||
                 context.Metadata.ModelType.GetTypeInfo().IsAbstract) &&
                (context.BindingInfo.BindingSource == null ||
                !context.BindingInfo.BindingSource
                .CanAcceptDataFrom(BindingSource.Services)))
            {
                var propertyBinders = new System.Collections.Generic.Dictionary<ModelMetadata, IModelBinder>();

                foreach (var property in context.Metadata.Properties)
                {
                    propertyBinders.Add(property, context.CreateBinder(property));
                }

                return new AbstractsModelBinder(propertyBinders, context.Services.GetService<Microsoft.Extensions.Logging.ILoggerFactory>());
            }

            return null;
        }
    }
}
