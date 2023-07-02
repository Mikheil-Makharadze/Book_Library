using Core.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.IdentityDB
{
    public static class IdentityDBContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //User
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var admin = await userManager.FindByEmailAsync("admin@bookLibrary.com");
                if (admin == null)
                {
                    var newAdmin = new User()
                    {
                        UserName = "admin@bookLibrary.com",
                        DisplayName = "Admin",
                        Email = "admin@bookLibrary.com"
                    };

                    var result1 = await userManager.CreateAsync(newAdmin, "Coding@1234?");
                }
            }
        }
    }
}
