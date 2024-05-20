using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Identities
{
    public class IdentityContext: IdentityDbContext<UserAddProperties>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }


    }
}
