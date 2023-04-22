﻿using ConferencePlanning.Data;
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

        var result = new ApplicationUser
        {   
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            UserSurname = user.UserSurname
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