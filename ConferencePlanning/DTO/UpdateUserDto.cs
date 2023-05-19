namespace ConferencePlanning.DTO;

public class UpdateUserDto
{
    public string Id { get; set; }
    
    public string UserName { get; set; }
    
    public string UserSurname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public string? Position { get; set; }
    
    public string? OrganizationName { get; set; }
}