using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;

namespace ConferencePlanning.Services.AccountService;

public interface IRegistrationService
{
    public Task<UserDto?> ModeratorRegistration(ModeratorRegistrationDto moderatorRegisterDto);

    public Task<UserDto?> UserRegistration(RegisterDto registerDto);
}