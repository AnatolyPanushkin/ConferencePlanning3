using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.AspNetCore.Identity;

namespace ConferencePlanning.Services.AccountService;

public class RegistrationService:IRegistrationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserDto?> ModeratorRegistration(RegisterDto registerDto)
    {
        var moderator = new ApplicationUser
        {
            UserName = registerDto.UserName,
            UserSurname = registerDto.UserSurname,
            Email = registerDto.Email,
            Role = "Moderator",
        };

        IdentityResult moderatorResult = _userManager.CreateAsync(moderator, registerDto.Password).Result;
                
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(moderator);
                
        if (moderatorResult.Succeeded)
        {
            _userManager.AddToRoleAsync(moderator, "Moderator").Wait();
            return new UserDto
            {
                DisplayName = moderator.UserSurname,
                Token = token,
                UserName = moderator.UserName,
                Role = moderator.Role
            };
        }

        return null;
    }

    public async Task<UserDto?> UserRegistration(RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            UserSurname = registerDto.UserSurname,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Role = "User",
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (result.Succeeded)
        {
            return new UserDto
            {
                DisplayName = user.UserSurname,
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            };
        }

        return null;
    }
}