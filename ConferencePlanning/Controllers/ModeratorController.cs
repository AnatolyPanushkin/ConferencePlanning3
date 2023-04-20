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
            OrganizationName = moderator.OrganizationName,
            Email = moderator.Email
        };
        
        return Ok(result);
    }

    [HttpPut("updateModerator")]
    public async Task<ActionResult> UpdateModerator(ModeratorEditDto moderatorDto)
    {

        var updateModerator = await _context.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(moderatorDto.Id));

        updateModerator.OrganizationName = moderatorDto.OrganizationName;
        updateModerator.Email = moderatorDto.Email;
        
        await _context.SaveChangesAsync();

        return Ok(moderatorDto);
    }
}