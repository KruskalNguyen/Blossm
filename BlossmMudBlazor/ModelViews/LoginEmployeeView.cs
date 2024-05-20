using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlossmAPI.ModelViews
{
    public class LoginEmployeeView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
