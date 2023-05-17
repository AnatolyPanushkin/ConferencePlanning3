 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Data.Entities;

public class ApplicationUser:IdentityUser
{
    public ApplicationUser()
    {
        UsersConferences = new HashSet<UsersConferences>();
    }

    public string UserSurname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public string? Position { get; set; }
    public string Role { get; set; }
    
    public string? OrganizationName { get; set; }
    
    public ICollection<Conference> Conferences { get; set; } = new List<Conference>();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<UsersConferences>  UsersConferences{ get; set; }
    
    public Questionnaire Questionnaire { get; set; }
    public Guid PhotoId { get; set; }

}