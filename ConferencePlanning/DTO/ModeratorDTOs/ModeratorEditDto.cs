using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class ModeratorEditDto
{
    public string Id { get; set; }

    public string OrganizationName { get; set; }
    
    public string Position { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
}