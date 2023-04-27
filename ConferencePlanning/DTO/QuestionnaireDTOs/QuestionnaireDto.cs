namespace ConferencePlanning.DTO.QuestionnaireDTOs;

public class QuestionnaireDto
{
    public string DockladTheme { get; set; }
    
    public string ScientificDegree { get; set; }
    
    public bool Type { get; set; }
    
    public string UserId { get; set; }
    
    public Guid ConferenceId { get; set; }
}