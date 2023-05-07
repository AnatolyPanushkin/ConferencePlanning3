namespace ConferencePlanning.DTO.SectionDTOs;

public class SectionCreateDto
{
    public string Name { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}