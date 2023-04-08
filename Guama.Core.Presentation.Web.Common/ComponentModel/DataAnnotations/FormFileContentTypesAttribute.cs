using System.Linq;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations
{
    public class FormFileContentTypesAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public string[] ContentTypes { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (ContentTypes.Length <= 0) return true;

            if (value is Microsoft.AspNetCore.Http.IFormFile)
            {
                var formFile = value as Microsoft.AspNetCore.Http.IFormFile;

                return ContentTypes.Any(item => item.Equals(formFile.ContentType));
            }

            return false;
        }
    }
}
