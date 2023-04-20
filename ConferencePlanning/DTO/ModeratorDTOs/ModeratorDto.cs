using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class ModeratorDto
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string OrganizationName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}