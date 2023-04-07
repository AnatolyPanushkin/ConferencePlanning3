using System.Reflection.Metadata.Ecma335;
using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Services.ConferenceServices;

public class ConferenceService : IConferenceService
{
    private readonly ConferencePlanningContext _context;
    private readonly IConfiguration _configuration;

    public ConferenceService(ConferencePlanningContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public IEnumerable<Conference> GetAllConferences()
    {
        return _context.Conferences.ToList();
    }
    

    public async Task<Conference> GetConference(Guid id)
    {
        var result = await _context.Conferences.FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task<Conference> AddNewConference(ConferenceDto conferenceDto)
    {
        var newConference = new Conference()
        {
            Id = Guid.NewGuid(),
            Name = conferenceDto.Name,
            ConferenceTopic = conferenceDto.ConferenceTopic,
            
        };
        _context.Conferences.Add(newConference);
        await _context.SaveChangesAsync();

        return newConference;
    }

    public async Task<string> GetPhotoName(Guid id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);

        return photo.Name;
    }
}