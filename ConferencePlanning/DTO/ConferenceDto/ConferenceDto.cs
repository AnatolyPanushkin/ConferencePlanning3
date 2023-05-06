using System.Text.Json.Serialization;

namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceDto
{
    
    public Guid Id { get; set; }
    public string Name { get; set;}
    
    public string Type { get; set; }
    public string ShortTopic { get; set; }
    public string FullTopic { get; set; }
    public string Addres { get; set; }
    public string City { get; set; }
    
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    //public string Organizer { get; set; }

    public string ModeratorId { get; set; }
    public string ImgUrl { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
    
}