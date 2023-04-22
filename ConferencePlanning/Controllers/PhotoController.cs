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

    [HttpGet("getConferencePhotoById{confId}")]
    public async Task<ActionResult> GetConferencePhotoById(Guid confId)
    {
        var photo =await _service.GetPhotoName(confId);
        
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{photo}";
        
        return Ok(HttpContext.Response.SendFileAsync(fullPath));
    }

    [HttpPost("addNewUserPhotoById{userId}")]
    public async Task<ActionResult> AddNewUserPhotoById(string id, IFormFile formFile)
    {
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{formFile.FileName}";
        
        var photo = await _service.AddNewUserPhoto(id,formFile.FileName);
        
        var fileStream = new FileStream(fullPath, FileMode.Create);
        
        await formFile.CopyToAsync(fileStream);

        return Ok(photo);
    }
}