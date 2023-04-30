using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceCreateDto
{
    public string Name { get; set;}
    public string Type { get; set; }
    
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; set; }
    
    public string ModeratorId { get; set; }
}