using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;

namespace ConferencePlanning.Mappers;

public static class UserToModeratorDto
{
    public static ModeratorDto MapUserToModeratorDto(this ApplicationUser applicationUser)
    {
        var dto = new ModeratorDto
        {
            Id = applicationUser.Id,
            OrganizationName = applicationUser.OrganizationName,
            Position = applicationUser.Position,
            Email = applicationUser.Email
        };

        return dto;
    }
}