
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Data.Entities;

public class ApplicationUser:IdentityUser
{
    public string UserSurname { get; set; }
    
    public string Bio { get; set; }
}