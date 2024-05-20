using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlossmAPI.ModelViews
{
    public class RegisterUserView
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Phone invalid")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        public string RePassword { get; set; }

    }
}
