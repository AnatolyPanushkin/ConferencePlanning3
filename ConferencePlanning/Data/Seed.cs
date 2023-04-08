using ConferencePlanning.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConferencePlanning.Data;

public class Seed
{
    public static async Task SeedData(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
    {
        
        if (!userManager.Users.Any())
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser {UserSurname = "Jhon", UserName = "Jhon", Email = "jhon@mail.ru",Bio = "Jhon"},
                new ApplicationUser {UserSurname = "Bob", UserName = "Bob", Email = "bob@mail.ru",Bio = "Jhon"},
                new ApplicationUser {UserSurname = "Tom", UserName = "Tom", Email = "tom@mail.ru",Bio = "Jhon"}
            };

            foreach (var user in users)
            {
               await userManager.CreateAsync(user, "Pa$$w0rd");
            }
            
        }
        
        var appUserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager != null && !roleManager.RoleExistsAsync("Admin").Result)
        {
            roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
        }
                
        if (appUserManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin@example.com",
                UserSurname = "Admin",
                Email = "admin@example.com",
                Bio = "admin"
            };

            IdentityResult result = appUserManager.CreateAsync(user, "P@ssw0rd").Result;
 
            if (result.Succeeded)
            {
                appUserManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
        
        
        if (roleManager != null && !roleManager.RoleExistsAsync("Moderator").Result)
        {
            roleManager.CreateAsync(new IdentityRole { Name = "Moderator" }).Wait();
        }
                
        if (appUserManager.FindByEmailAsync("moderator@example.com").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "moderator@example.com",
                UserSurname = "Moderator",
                Email = "moderator@example.com",
                Bio = "moderator"
            };

            IdentityResult result = appUserManager.CreateAsync(user, "P@ssw0rd").Result;
 
            if (result.Succeeded)
            {
                appUserManager.AddToRoleAsync(user, "Moderator").Wait();
            }
        }
    }
}