namespace ConferencePlanning.Data.Entities;

public class Conference
{
    public Guid Id { get; set; }
    public string Name { get; set;}
    public string ConferenceTopic { get; set; }

    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    [System.Text.Json.Serialization.JsonIgnore]
    public List<UsersConferences> UsersConferences { get; set; } = new List<UsersConferences>();
    public Guid PhotoId { get; set; }
}