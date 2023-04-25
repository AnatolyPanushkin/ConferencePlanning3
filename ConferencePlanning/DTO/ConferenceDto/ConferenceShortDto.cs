namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceShortDto
{
    public Guid Id { get; set; }
    public string Name { get; set;}
    public string Type { get; set; }
    public DateOnly Date { get; set; }
    public string ImgUrl { get; set; }
}