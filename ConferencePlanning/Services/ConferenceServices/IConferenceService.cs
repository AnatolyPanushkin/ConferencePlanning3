using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;

namespace ConferencePlanning.Services.ConferenceServices;

public interface IConferenceService
{
    IEnumerable<Conference> GetAllConferences();

    Task<Conference> GetConference(Guid id);

    Task<Conference> AddNewConference(ConferenceDto conferenceDto);
    public Task<string> GetPhotoName(Guid id);
}