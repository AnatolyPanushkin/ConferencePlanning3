 
namespace ConferencePlanning.Data.Entities;

public enum StatusValue
{
    Accepted,
    NotAccepted
}
public class Questionnaire
{
    public Guid Id { get; set; }
    
    public string DockladTheme { get; set; }
    
    public string ScientificDegree { get; set; }
    
    public string Type { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public StatusValue Status { get; set; }
}