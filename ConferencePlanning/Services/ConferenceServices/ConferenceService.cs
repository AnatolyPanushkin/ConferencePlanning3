using System.Reflection.Metadata.Ecma335;
using ConferencePlanning.Data;
using ConferencePlanning.Mappers;
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
        var newConference = conferenceDto.MapConferenceDtoToConference();
        newConference.Id = Guid.NewGuid();
        
        _context.Conferences.Add(newConference);
        await _context.SaveChangesAsync();

        return newConference;
    }

    public async Task<bool> AddUser(Guid id, string userId)
    {
        await _context.UsersConferences.AddAsync(new() { UserId = userId, ConferenceId = id });
        await _context.SaveChangesAsync();
        return true;

        /*var conference = await _context.Conferences.FirstOrDefaultAsync(conf => conf.Id == id);
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        
        conference.Users.Add(user);
        user.Conferences.Add(conference);
        
        await _context.UsersConferences.AddAsync(new() { UserId = userId, ConferenceId = id });
        
        await _context.SaveChangesAsync();
        return true;*/
    }

    public async Task<string> GetPhotoName(Guid id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);

        return photo.Name;
    }

    public async Task<Conference> GetConferenceWithSections(Guid id)
    {
        var result = await _context.Conferences
            .Include(conf=>conf.Sections)
            .FirstOrDefaultAsync(conf => conf.Id == id);

        return result;
    }

    public async Task<IEnumerable<Conference>> GetUserConference(string id)
    {
        /*var userConferences = _context.UsersConferences
            .Where(uc => uc.UserId.Equals(id))
            .Include(uc=>uc.Conference);
        
        
        var conferences = _context.Conferences.Where(conf=>conf.Id==userConferences.)*/


        var user = await _context.Users.Include(user => user.Conferences)
            .FirstOrDefaultAsync(user => user.Id.Equals(id));
        return user.Conferences;
    }
}