using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;

[Route("api/moderator")]
[ApiController]
public class ModeratorController:ControllerBase
{
    private readonly ConferencePlanningContext _context;

    public ModeratorController(ConferencePlanningContext context)
    {
        _context = context;
    }

    [HttpGet("getModeratorById")]
    public async Task<ActionResult> GetModeratorById(string id)
    {
        var moderator = await _context.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(id));

        if (moderator==null)
        {
            return BadRequest("moderator not exist");
        }

        var result = new ModeratorDto
        {   
            Id = moderator.Id,
            UserName = moderator.UserName,
            UserSurname = moderator.UserSurname,
            Patronymic = moderator.Patronymic,
            OrganizationName = moderator.OrganizationName,
            Position = moderator.Position,
            Email = moderator.Email,
            PhoneNumber = moderator.PhoneNumber
        };
        
        return Ok(result);
    }

    [HttpPut("updateModerator")]
    public async Task<ActionResult> UpdateModerator(ModeratorEditDto moderatorDto)
    {

        var updateModerator = await _context.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(moderatorDto.Id));

        if (updateModerator!=null)
        {
            updateModerator.OrganizationName = moderatorDto.OrganizationName;
            updateModerator.Email = moderatorDto.Email;
            updateModerator.Position = moderatorDto.Position;

            if (moderatorDto.PhoneNumber!=null)
            {
                updateModerator.PhoneNumber = moderatorDto.PhoneNumber;
            }
            
            if (moderatorDto.UserName!=null)
            {
                updateModerator.UserName = moderatorDto.UserName;
            }
            
            if (moderatorDto.UserSurname!=null)
            {
                updateModerator.UserSurname = moderatorDto.UserSurname;
            }
            
            if (moderatorDto.Patronymic!=null)
            {
                updateModerator.Patronymic = moderatorDto.Patronymic;
            }
        }
        
        /*var updateModerator = new ApplicationUser
        {
            Id = moderatorDto.Id,
            UserName = moderatorDto.UserName,
            UserSurname = moderatorDto.UserSurname,
            Patronymic = moderatorDto.Patronymic,
            OrganizationName = moderatorDto.OrganizationName,
            Position = moderatorDto.Position,
            Email = moderatorDto.Email
        };
        
        _context.Users.Update(updateModerator);*/
        await _context.SaveChangesAsync();

        return Ok(moderatorDto);
    }
}