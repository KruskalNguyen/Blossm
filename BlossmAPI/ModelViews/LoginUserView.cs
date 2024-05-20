using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlossmAPI.ModelViews
{
    public class LoginUserView
    {
        [Required]
        [Phone]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Phone invalid")]
        public string PhoneNumber { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
