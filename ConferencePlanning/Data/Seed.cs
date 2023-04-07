using ConferencePlanning.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConferencePlanning.Data;

public class Seed
{
    public static async Task SeedData(UserManager<User> userManager, IServiceProvider serviceProvider)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new User {DisplayName = "Jhon", UserName = "Jhon", Email = "jhon@mail.ru",Bio = "Jhon"},
                new User {DisplayName = "Bob", UserName = "Bob", Email = "bob@mail.ru",Bio = "Jhon"},
                new User {DisplayName = "Tom", UserName = "Tom", Email = "tom@mail.ru",Bio = "Jhon"}
            };

            foreach (var user in users)
            {
               await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
        var appUserManager = serviceProvider.GetService<UserManager<User>>();
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager != null && !roleManager.RoleExistsAsync("Admin").Result)
        {
            roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
        }
                
        if (appUserManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            var user = new User
            {
                UserName = "admin@example.com",
                DisplayName = "Admin",
                Email = "admin@example.com",
                Bio = "admin"
            };

            IdentityResult result = appUserManager.CreateAsync(user, "P@ssw0rd").Result;
 
            if (result.Succeeded)
            {
                appUserManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}