namespace ConferencePlanning.Data.Entities;

public class User
{
    public User()
    {
        
    }

    public string UserName { get; set; }
    public string UserSurname { get; set; }
    
    public string Role { get; set; }
    
    public string? OrganizationName { get; set; }
    public List<Conference> Conferences { get; set; } = new List<Conference>();
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<UsersConferences>  UsersConferences{ get; set; }
    
    public Guid PhotoId { get; set; }
    
}