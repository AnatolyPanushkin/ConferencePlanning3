using System.Reflection.Metadata.Ecma335;
using ConferencePlanning.Data;
using ConferencePlanning.Mappers;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.ConferenceDto;
using ConferencePlanning.DTO.QuestionnaireDTOs;
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
    public async Task<ICollection<ConferenceDto>> GetAllConferences()
    {
        var conferences = await _context.Conferences
            .Select(c => new ConferenceDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Date = c.Date,
                    Categories = c.Categories,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    Addres = c.Addres,
                    ModeratorId = c.ModeratorId,
                    ImgUrl = $"getConferencePhotoById{c.Id}"
                })
            .ToListAsync();


        return conferences;
    }
    

    public async Task<Conference> GetConference(Guid id)
    {
        var result = await _context.Conferences.FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task<Conference> AddNewConference(ConferenceCreateDto conferenceDto)
    {
        var newConference = new Conference
        {
            Id = Guid.NewGuid(),
            Name = conferenceDto.Name,
            Type = conferenceDto.Type,
            Date = conferenceDto.Date,
            ModeratorId = conferenceDto.ModeratorId
        };

        _context.Conferences.Add(newConference);
        await _context.SaveChangesAsync();

        return newConference;
    }

    public async Task<Conference> UpdateConference(ConferenceDto confDto)
    {
        var updateConf = confDto.MapConferenceDtoToConference();
        updateConf.Id = confDto.Id;
        
        _context.Conferences.Update(updateConf);
        await _context.SaveChangesAsync();

        return updateConf;
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

    public async Task<ICollection<ConferenceShortDto>> GetUserConferences(string id)
    {
        var conf = await _context.UsersConferences.Where(c => c.UserId.Equals(id))
            .Select(c => new ConferenceShortDto
            {
                Id = c.Conference.Id,
                Name = c.Conference.Name,
                Type = c.Conference.Type,
                Date = c.Conference.Date,
                ImgUrl = $"getConferencePhotoById{c.Conference.Id}"
            }).ToListAsync();
        
        return conf;
    }

    public async Task<ICollection<ConferenceShortDto>> GetModeratorConferences(string id)
    {
        var conf = await _context.Conferences.Where(c => c.ModeratorId.Equals(id))
            .Select(c => new ConferenceShortDto
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                Date = c.Date,
                ImgUrl = $"getConferencePhotoById{c.Id}"
            }).ToListAsync();
        
        return conf;
    }
    public async Task<ICollection<ConferenceQuestionnaireDto>> GetConferenceQuestionnaire(Guid confId)
    {
        var questionnaires = await _context.PotentialParticipants.Where(p => p.ConferenceId == confId)
            .Select(p => new ConferenceQuestionnaireDto
            {
                UserName = p.User.UserName,
                UserSurname = p.User.UserSurname,
                Position = p.User.Position,
                DockladTheme = p.User.Questionnaire.DockladTheme,
                ScientificDegree = p.User.Questionnaire.ScientificDegree,
                Type = p.User.Questionnaire.Type,
                UserId = p.UserId,
                ConferenceId = p.ConferenceId
            }).ToListAsync();

        return questionnaires;
    }

    public async Task<ConferenceDto> DeleteConference(Guid confId)
    {
        var conf = await _context.Conferences.FirstOrDefaultAsync(c => c.Id == confId);

        if (conf == null)
        {
            return null;
        }

        var communicationUsers = await _context.UsersConferences.Where(uc => uc.ConferenceId == confId)
            .Select(uc => uc).ToListAsync();

        foreach (var uc in communicationUsers)
        {
            _context.UsersConferences.Remove(uc);
        }
        
        var communicationParticipants = await _context.PotentialParticipants.Where(pp => pp.ConferenceId == confId)
            .Select(pp => pp).ToListAsync();
        foreach (var pp in communicationParticipants)
        {
            _context.PotentialParticipants.Remove(pp);
        }

        _context.Conferences.Remove(conf);
        await _context.SaveChangesAsync();

        return new ConferenceDto
        {  
            Name = conf.Name,
            Type = conf.Type
        };
    }
    
}