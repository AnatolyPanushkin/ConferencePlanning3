namespace ConferencePlanning.Data.Entities;

public class Conference
{
    public Guid Id { get; set; }
    public string Name { get; set;}
    public string ConferenceTopic { get; set; }
    
    public Guid PhotoId { get; set; }
}