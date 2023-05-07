﻿using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;

[ApiController]
[Route("api/section")]
public class SectionController : ControllerBase
{
    private readonly ConferencePlanningContext _context;

    public SectionController(ConferencePlanningContext context)
    {
        _context = context;
    }

    [HttpGet("getSections")]
    public async Task<ActionResult> GetSections(Guid confId)
    {
        var sections = await _context.Sections.Where(s => s.Conference.Id == confId)
            .Select(s => new Section
            {
                Id = s.Id,
                Name = s.Name,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            }).ToListAsync();

        return Ok(sections);
    }

    [HttpPost("addSections")]
    public async Task<ActionResult> AddSections(ICollection<SectionDto> sections, Guid ConferenceId)
    {
        var conference = _context.Conferences.FirstOrDefault(conf => conf.Id == ConferenceId);
        foreach (var section in sections)
        {
            var newSection = new Section
            {
                Id = new Guid(),
                Name = section.Name,
                StartTime = section.StartTime,
                EndTime = section.EndTime,
                Conference = conference
            };
            var result = await _context.Sections.AddAsync(newSection);
        }

        await _context.SaveChangesAsync();

        return Ok(sections);
    }

    [HttpPost("addSection")]
    public async Task<ActionResult> AddSection(SectionDto sectionDto, Guid ConferenceId)
    {
        var conference = _context.Conferences.FirstOrDefault(conf => conf.Id == ConferenceId);
        
        var result = await _context.Sections.AddAsync(
            new Section
            {
                Id = new Guid(),
                Name = sectionDto.Name,
                StartTime = sectionDto.StartTime,
                EndTime = sectionDto.EndTime,
                Conference = conference
            });
        await _context.SaveChangesAsync();

        return Ok(sectionDto);
    }
    
}