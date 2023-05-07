namespace ConferencePlanning.DTO.SectionDTOs;

public class SectionDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}