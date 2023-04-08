namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract
{
    public interface IConcretModelBindHelper
    {
        void DoConcretBind(object model, Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext bindingContext);
    }
}
