namespace ConferencePlanning.DTO;

public class SectionDto
{
    public string Name { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}