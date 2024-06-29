using Microsoft.AspNetCore.Identity;

namespace Ecommerce.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Azazy",
                    Email = "elazazy11@yahoo.com",
                    UserName = "AhmedELazazy",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "ELazazy",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
