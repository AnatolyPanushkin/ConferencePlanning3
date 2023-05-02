using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class ModeratorRegistrationDto
{
    /*[Required]
    public string UserSurname { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Patronymic { get; set; }
    
    [Required]
    public string Position { get; set; }*/
    
    [Required]
    public string OrganizationName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}