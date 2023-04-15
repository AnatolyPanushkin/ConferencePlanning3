namespace ConferencePlanning.Data.Entities;

public class Section
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public Conference Conference { get; set; }
}