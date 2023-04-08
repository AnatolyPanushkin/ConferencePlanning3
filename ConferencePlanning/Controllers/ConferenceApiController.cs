using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using ConferencePlanning.Services.ConferenceServices;
using ConferencePlanning.Services.PhotosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ConferencePlanning.Controllers;

[Route("api/conferences")]
[ApiController]
public class ConferenceApiController:ControllerBase
{
    private readonly IConferenceService _service;
    private readonly IConfiguration _configuration;

    public ConferenceApiController(IConferenceService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    /*[HttpGet("getAllConferences")]
    public IActionResult GetAllConferences()
    {
        return Ok( _service.GetAllConferences());
    }*/
  
    [Authorize(Roles = "Admin")]
    [HttpGet("getAllConferences")]
    
    public IActionResult GetConferences()
    {
        var context = HttpContext.User;
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
    
}