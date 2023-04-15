namespace ConferencePlanning.Data.Entities;

public class Conference
{
    public Guid Id { get; set; }
    public string Name { get; set;}
    
    public string ShortTopic { get; set; }
    
    public string FullTopic { get; set; }
    public string Addres { get; set; }
    public string City { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
    
    public string Organizer { get; set; }

    public List<string> Categories { get; set; } = new List<string>();

    public Guid PhotoId { get; set; }
    
    
    
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    [System.Text.Json.Serialization.JsonIgnore]
    public List<UsersConferences> UsersConferences { get; set; } = new List<UsersConferences>();
    
}