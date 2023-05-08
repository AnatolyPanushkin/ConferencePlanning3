using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.SectionDTOs;
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
    public async Task<ActionResult> AddSections(ICollection<SectionCreateDto> sections, Guid ConferenceId)
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
    public async Task<ActionResult> AddSection(SectionCreateDto sectionDto, Guid conferenceId)
    {
        var conference = _context.Conferences.FirstOrDefault(conf => conf.Id == conferenceId);
        
        var section = new Section
            {
                Id = new Guid(),
                Name = sectionDto.Name,
                StartTime = sectionDto.StartTime,
                EndTime = sectionDto.EndTime,
                Conference = conference
            };
        await _context.Sections.AddAsync(section);
        await _context.SaveChangesAsync();
        
        return Ok(section);
    }

    [HttpPut("updateSections")]
    public async Task<ActionResult> UpdateSections(List<SectionDto> sections)
    {
        foreach (var sec in sections)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == sec.Id);
            
            if (section!=null)
            {
                section.Name = sec.Name;
                section.StartTime = sec.StartTime;
                section.EndTime = sec.EndTime;
                
                _context.Sections.Update(section);
            }
        }

        await _context.SaveChangesAsync();

        return Ok(sections);
    }
    
    [HttpDelete("deleteSection")]
    public async Task<ActionResult> DeleteSections(Guid secId)
    {
        var section = _context.Sections.FirstOrDefault(s => s.Id == secId);

        
        _context.Sections.Remove(section);
        
        await _context.SaveChangesAsync();

        return Ok(section);
    }
}