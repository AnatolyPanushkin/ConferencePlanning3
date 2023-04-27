using System.Security.Principal;

namespace ConferencePlanning.DTO;

public class UserDto
{
    public string Id { get; set; }
    public string Surname { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
    
    public string OrganizationName { get; set; }
}
