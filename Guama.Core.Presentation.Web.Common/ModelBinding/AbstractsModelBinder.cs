using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.ModelBinding
{
    public sealed class AbstractsModelBinder : ComplexTypeModelBinder
    {
        public AbstractsModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory) : base(propertyBinders, loggerFactory)
        {
        }

        protected override object CreateModel(ModelBindingContext bindingContext)
        {
            var model = bindingContext.HttpContext.RequestServices.GetService(bindingContext.ModelType);

            var modelBindHelper = bindingContext.HttpContext.RequestServices.GetService(typeof(IConcretModelBindHelper)) as IConcretModelBindHelper;

            modelBindHelper.DoConcretBind(model, bindingContext);

            return model;
        }
    }
}
