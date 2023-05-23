using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;

[Route("api/user")]
[ApiController]
public class UserController:ControllerBase
{
    private readonly ConferencePlanningContext _context;

    public UserController(ConferencePlanningContext context)
    {
        _context = context;
    }

    [HttpGet("getUserById")]
    public async Task<ActionResult> GetUserById(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(id));

        if (user==null)
        {
            return BadRequest("moderator not exist");
        }
        
        return Ok(user);
    }

    [HttpPut("updateUser")]
    public async Task<ActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(updateUserDto.Id));

        if (user!=null)
        {
            user.UserName = updateUserDto.UserName;
            user.UserSurname = updateUserDto.UserSurname;
            user.Patronymic = updateUserDto.Patronymic;
            user.Position = updateUserDto.Position;
            user.OrganizationName = updateUserDto.OrganizationName;
            
            _context.Users.Update(user);
        }

        await _context.SaveChangesAsync();

        return Ok(updateUserDto);
    }
    
}