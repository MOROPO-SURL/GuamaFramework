using MOROPO.Framework.Guama.Core.Presentation.Web.Common.Extensions;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations
{
    public class FormFileMinLengthAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public long Length { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (Length <= 0) return true;

            if (value is Microsoft.AspNetCore.Http.IFormFile)
            {
                var formFile = value as Microsoft.AspNetCore.Http.IFormFile;

                return formFile.Length <= Length;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "El tamaño del archivo especificado en el campo '" + name + "' no debe ser inferior a " + Length.ToFileSizeString();
        }
    }
}
