
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Data.Entities;

public class ApplicationUser:IdentityUser
{
    public string UserSurname { get; set; }
    
    public string Role { get; set; }
    
    public List<Conference> Conferences { get; set; } = new List<Conference>();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public List<UsersConferences> UsersConferences { get; set; } = new List<UsersConferences>();

}