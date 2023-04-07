using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;

namespace ConferencePlanning.Services.PhotosServices;

public interface IPhotosService
{
    public Task<int> AddNewConferencePhoto(Guid conferenceId, string photoName);

    public Task<string> GetPhotoName(Guid id);
}