namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceUpdateDto
{
    public string ShortTopic { get; set; }
    public string FullTopic { get; set; }
    public string Addres { get; set; }
    public string City { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Organizer { get; set; }

    public List<string> Categories { get; set; } = new List<string>();
}