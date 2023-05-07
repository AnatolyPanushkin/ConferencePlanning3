using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class ModeratorEditDto
{
    [Required]
    public string Id { get; set; }
    public string UserSurname { get; set; }
    public string UserName { get; set; }
    public string Patronymic { get; set; }
    
    public string OrganizationName { get; set; }
    
    public string Position { get; set; }
   
    [EmailAddress]
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}