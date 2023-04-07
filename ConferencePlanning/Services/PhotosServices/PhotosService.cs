﻿using ConferencePlanning.Data;
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
    
    public async Task<string> GetPhotoName(Guid id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);

        return photo.Name;
    }
}