using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Models.View
{
    public class UserChangePasswordViewModel
    {
        public UserChangePasswordViewModel()
        {
            ChangePasswordRequired = true;
        }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [Required, StringLength(80, MinimumLength = 6), DataType(DataType.Password), Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Display(Name = "ConfirmPassword"), Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "ChangePasswordRequired")]
        public bool ChangePasswordRequired { get; set; }
    }
}
