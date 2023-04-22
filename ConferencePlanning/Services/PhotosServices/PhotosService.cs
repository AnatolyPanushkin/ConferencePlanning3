using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ConferencePlanning.Services.PhotosServices;

public class PhotosService:IPhotosService
{
    private readonly ConferencePlanningContext _context;

    public PhotosService(ConferencePlanningContext context)
    {
        _context = context;
    }

    public async Task<int> AddNewConferencePhoto(Guid conferenceId, string photoName)
    {
        var photo = new Photo()
        {
            Id = new Guid(),
            Name = photoName
        };
        
        _context.Photos.Add(photo);

        var conf = await _context.Conferences.FirstOrDefaultAsync(conf => conf.Id == conferenceId);

        conf.PhotoId = photo.Id;
        
        var result = await _context.SaveChangesAsync();

        return result;
    }

    public async Task<int> AddNewUserPhoto(string id, string photoName)
    {
        var photo = new Photo()
        {
            Id = new Guid(),
            Name = photoName
        };
        
        _context.Photos.Add(photo);

        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));

        user.PhotoId = photo.Id;
        
        var result = await _context.SaveChangesAsync();

        return result;
    }

    public async Task<string> GetPhotoName(Guid confId)
    {
        var conf = await _context.Conferences.FirstOrDefaultAsync(conf => conf.Id == confId);

        if (conf!=null)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == conf.PhotoId);
            return photo.Name;
        }

        return null;
    }
}