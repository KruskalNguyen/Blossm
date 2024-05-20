using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BlossmAPI.Attributes;

namespace BlossmAPI.ModelViews
{
    public class RegisterEmployeeView
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Phone invalid")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be greater than start date.")]
        public DateTime EndDate { get; set; }
        [Required]
        public int Branch { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        public string RePassword { get; set; }
    }
}
