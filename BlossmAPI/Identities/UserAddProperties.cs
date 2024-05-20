using Microsoft.AspNetCore.Identity;

namespace BlossmAPI.Identities
{
    public class UserAddProperties:IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
