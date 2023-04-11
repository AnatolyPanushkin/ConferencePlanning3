using System.Security.Principal;

namespace ConferencePlanning.DTO;

public class UserDto
{
    public string DisplayName { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
}
