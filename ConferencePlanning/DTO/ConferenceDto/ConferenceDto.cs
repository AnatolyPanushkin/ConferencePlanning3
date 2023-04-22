namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceDto
{
    //тип конференции, состоянии конференции
    public Guid Id { get; set; }
    public string Name { get; set;}
    public string ShortTopic { get; set; }
    public string FullTopic { get; set; }
    
    //необязательные поля
    public string Addres { get; set; }
    public string City { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Organizer { get; set; }

    public List<string> Categories { get; set; } = new List<string>();
    
}