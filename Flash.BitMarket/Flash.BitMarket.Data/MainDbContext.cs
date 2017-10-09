using Flash.BitMarket.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Flash.BitMarket.Data
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public MainDbContext()
            : base("MainDb", throwIfV1Schema: false)
        {
        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }
    }
}
