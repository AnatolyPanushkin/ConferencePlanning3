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

    [HttpGet("getConferencePhotoById")]
    public async Task<ActionResult<Task>> GetConferencePhotoById()
    {
        //var photo =await _service.GetPhotoName(id);
        
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\img1.jpg";
        
        return HttpContext.Response.SendFileAsync("C:\\Users\\User\\RiderProjects\\ConferencePlanning3\\ConferencePlanning\\ImgFiles\\ConferencesImg\\img2.jpg");
    }
}