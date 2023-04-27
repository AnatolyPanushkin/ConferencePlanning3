namespace ConferencePlanning.Data.Entities;

public class Questionnaire
{
    public Guid Id { get; set; }
    
    public string DockladTheme { get; set; }
    
    public string ScientificDegree { get; set; }
    
    public bool Type { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}