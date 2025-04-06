using AutoGenerator.Models;
using Microsoft.AspNetCore.Identity;
using Utilities;

namespace AutoGenerator.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                return;
            }
            await roleManager.CreateAsync(new ApplicationRole { Name = UserRole.Admin });
            await roleManager.CreateAsync(new ApplicationRole { Name = UserRole.SuperVisor });
            await roleManager.CreateAsync(new ApplicationRole { Name = UserRole.User });
        }



    }
}