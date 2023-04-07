
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Data.Entities;

public class User:IdentityUser
{
    public string DisplayName { get; set; }
    
    public string Bio { get; set; }
}