namespace ConferencePlanning.DTO.QuestionnaireDTOs;

public class ConferenceQuestionnaireDto
{
    public string UserName { get; set; }
    
    public string UserSurname { get; set; }
    
    public string Position { get; set; }
    
    public string DockladTheme { get; set; }
    
    public string ScientificDegree { get; set; }
    
    public string Type { get; set; }
    
    public string UserId { get; set; }
    
    public Guid ConferenceId { get; set; }
    
    public string Status { get; set; }
}