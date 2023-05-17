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
    
    public async Task<UserDto?> ModeratorRegistration(ModeratorRegistrationDto moderatorRegisterDto)
    {
        var moderator = new ApplicationUser
        {
            UserName = moderatorRegisterDto.OrganizationName,
            UserSurname = moderatorRegisterDto.OrganizationName,
            Patronymic = moderatorRegisterDto.OrganizationName,
            Position = moderatorRegisterDto.OrganizationName,
            OrganizationName = moderatorRegisterDto.OrganizationName,
            Email = moderatorRegisterDto.Email,
            Role = "Moderator"
        };

        IdentityResult moderatorResult = _userManager.CreateAsync(moderator, moderatorRegisterDto.Password).Result;
                
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(moderator);
                
        if (moderatorResult.Succeeded)
        {
            _userManager.AddToRoleAsync(moderator, "Moderator").Wait();
            return new UserDto
            {
                Id = moderator.Id,
                Surname = moderator.UserSurname,
                UserName = moderator.UserName,
                OrganizationName = moderator.OrganizationName,
                Token = token,
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
                Id = user.Id,
                Surname = user.UserSurname,
                UserName = user.UserName,
                Token = token,
                Role = user.Role
            };
        }

        return null;
    }
}