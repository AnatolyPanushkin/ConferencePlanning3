using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
// using ConferencePlanning.Data.Migrations;
using ConferencePlanning.DTO;
using ConferencePlanning.Services.ConferenceServices;
using ConferencePlanning.Services.PhotosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ConferencePlanning.Controllers;

[Route("api/conferences")]
[ApiController]
public class ConferenceApiController:ControllerBase
{

    private readonly IConferenceService _service;
    private readonly IPhotosService _photosService;
    private readonly IConfiguration _configuration;

    public ConferenceApiController(IConferenceService service, IConfiguration configuration, IPhotosService photosService)
    {
        _service = service;
        _configuration = configuration;
        _photosService = photosService;
    }

    /*[HttpGet("getAllConferences")]
    public IActionResult GetAllConferences()
    {
        return Ok( _service.GetAllConferences());
    }*/
  
    //[Authorize(Roles = "Admin")]
    [HttpGet("getAllConferences")]
    public IActionResult GetConferences()
    {
       
        return Ok( _service.GetAllConferences());
    }

    [HttpGet("getConferenceById")]
    public async Task<ActionResult> GetConferenceById(Guid id)
    {
        var conf = await _service.GetConference(id);

        var photoName = await _service.GetPhotoName(conf.PhotoId);
        
        var imageUrl = $"{_configuration["PathConferencePhoto"]}\\{photoName}";

        var result = new
        {
            conf,
            imageUrl
        };
        return Ok(result);
    }
    
    [HttpPost("addNewConference")]
    public async Task<ActionResult<Conference>> AddNewConference(ConferenceDto conferenceDto)
    {
        var conference = await _service.AddNewConference(conferenceDto);
        
        return Ok(conference);
    }

    [HttpPost("addNewConferenceWithImg")]
    public async Task<ActionResult<Conference>> AddNewConferenceWithImg([FromForm]ConferenceWithImgDto conferenceWithImgDto)
    {
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{conferenceWithImgDto.Img.FileName}";
        var fileStream = new FileStream(fullPath, FileMode.Create);
        await conferenceWithImgDto.Img.CopyToAsync(fileStream);
        
        var conference = await _service.AddNewConference(conferenceWithImgDto.ConferenceDto);
        var photo = await _photosService.AddNewConferencePhoto(conference.Id,conferenceWithImgDto.Img.FileName);

        var imgUrl = $"api/photos/getConferencePhotoById{conference.PhotoId}";

        var result = new
        {
            conference, imgUrl
        };
        return Ok(result);
       
    }
    
    [HttpPost("addUser")]
    public async Task<ActionResult> AddUser(Guid id, string userId)
    {
        var result = await _service.AddUser(id, userId);

        if (result==true)
        {
            return Ok();
        }

        return BadRequest("Not added");
    }
    
    

    [HttpGet("getConferenceWithSections")]
    public async Task<ActionResult> GetConferenceWithSections(Guid id)
    {
        var result =await _service.GetConferenceWithSections(id);

        return Ok(result);
    }

    [HttpGet("getUserConference")]
    public async Task<ActionResult> GetUserConference(string id)
    {
        var conferences = await _service.GetUserConference(id);
        return Ok(conferences);
    }
}