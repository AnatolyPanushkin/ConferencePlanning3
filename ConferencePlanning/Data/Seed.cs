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
                new ApplicationUser {UserSurname = "Jhon", UserName = "Jhon",Patronymic = "Jhon",Position = "user",Email = "jhon@mail.ru",Role = "User"},
                new ApplicationUser {UserSurname = "Bob", UserName = "Bob", Patronymic = "Jhon",Position = "user",Email = "bob@mail.ru",Role = "User"},
                new ApplicationUser {UserSurname = "Tom", UserName = "Tom", Patronymic = "Jhon",Position = "user",Email = "tom@mail.ru",Role = "User"}
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
                Patronymic = "Admin",
                Position = "Admin",
                Email = "admin@example.com",
                Role = "Admin"
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
                Patronymic = "Admin",
                Position = "Admin",
                Email = "moderator@example.com",
                Role = "Moderator"
            };

            IdentityResult result = appUserManager.CreateAsync(user, "P@ssw0rd").Result;
 
            if (result.Succeeded)
            {
                appUserManager.AddToRoleAsync(user, "Moderator").Wait();
            }
        }
    }
}