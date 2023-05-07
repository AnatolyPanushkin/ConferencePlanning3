using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class ModeratorDto
{
    [Required]
    public string Id { get; set; }
    public string UserSurname { get; set; }
    
    public string UserName { get; set; }
    
    public string Patronymic { get; set; }
    
    [Required]
    public string OrganizationName { get; set; }
    
    [Required]
    public string Position { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}