using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.ConferenceDto;


namespace ConferencePlanning.Services.ConferenceServices;

public interface IConferenceService
{
    Task<ICollection<ConferenceShortDto>> GetAllConferences();

    Task<Conference> GetConference(Guid id);

    Task<Conference> AddNewConference(ConferenceDto conferenceDto);

    public Task<bool> AddUser(Guid id, string userId);
    public Task<string> GetPhotoName(Guid id);

    public Task<Conference> GetConferenceWithSections(Guid id);

    public Task<ICollection<ConferenceShortDto>> GetUserConferences(string id);
}