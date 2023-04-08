using System.ComponentModel.DataAnnotations;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Security.Models.View
{
    public class ChangePasswordViewModel
    {
        [Required, DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required, StringLength(80, MinimumLength = 6), DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare("NewPassword")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
