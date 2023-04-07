using ConferencePlanning.DTO;
using ConferencePlanning.Services.PhotosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferencePlanning.Controllers;

[Route("api/photos")]
[ApiController]
[AllowAnonymous]
public class PhotoController:ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly IPhotosService _service;
    
    public PhotoController(IConfiguration configuration, IPhotosService service)
    {
        _configuration = configuration;
        _service = service;
    }
    
    [HttpPost("addNewConferencePhotoById{conferenceId}")]
    public async Task<ActionResult> AddNewConferencePhoto(Guid conferenceId, IFormFile formFile)
    {
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{formFile.FileName}";
        
        var photo = await _service.AddNewConferencePhoto(conferenceId,formFile.FileName);
        
        var fileStream = new FileStream(fullPath, FileMode.Create);
        
        await formFile.CopyToAsync(fileStream);

        return Ok(photo);
    }

    [HttpGet("getConferencePhotoById{id}")]
    public async Task<ActionResult<Task>> GetConferencePhotoById(Guid id)
    {
        var photo =await _service.GetPhotoName(id);
        
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{photo}";
        
        return HttpContext.Response.SendFileAsync(fullPath);
    }
}