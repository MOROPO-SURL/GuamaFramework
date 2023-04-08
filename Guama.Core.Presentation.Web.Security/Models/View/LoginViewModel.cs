using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Models.View
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "Verification Code")]
        public string CaptchaCode { get; set; }

        public bool RememberMe { get; set; }
    }
}