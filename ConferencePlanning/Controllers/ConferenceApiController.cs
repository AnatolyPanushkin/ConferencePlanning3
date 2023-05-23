using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.ConferenceDto;
// using ConferencePlanning.Data.Migrations;
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
    public async Task<ActionResult> GetConferences()
    {
        return Ok(await _service.GetAllConferences());
    }

    [HttpGet("getConferenceById")]
    public async Task<ActionResult> GetConferenceById(Guid id)
    {
        var conf = await _service.GetConference(id);

        //if (conf.PhotoId.ToString() != null)
        
            //var photoName = await _service.GetPhotoName(conf.PhotoId);

            //var imageUrl = $"getConferencePhotoById{conf.Id}";

            var result = new
            {
                conf
            };
            return Ok(result);
        
        /*else
        {
            return Ok(conf);
        }*/
    }

    [HttpGet("getUsers")]
    public async Task<ActionResult> GetUsers(Guid confId)
    {
        var users = await _service.GetUsers(confId);
        if (users!=null)
        {
            return Ok(users);
        }

        return BadRequest("No users in conference");
    }
    
    [HttpGet("getConferenceWithSections")]
    public async Task<ActionResult> GetConferenceWithSections(Guid id)
    {
        var result =await _service.GetConferenceWithSections(id);

        return Ok(result);
    }

    [HttpGet("getUserConferences")]
    public async Task<ActionResult> GetUserConferences(string id)
    {
        var conferences = await _service.GetUserConferences(id);
        return Ok(conferences);
    }

    [HttpGet("getPotentialParticipants")]
    public async Task<ActionResult> GetPotentialParticipants(Guid confId)
    {
        var potential = await _service.GetConferenceQuestionnaire(confId);

        return Ok(potential);
    }

    [HttpGet("getModeratorConferences")]
    public async Task<ActionResult> GetModeratorConferences(string moderatorId)
    {
        var moderatorConf= await _service.GetModeratorConferences(moderatorId);

        return Ok(moderatorConf);
    }

    [HttpPost("addNewConference")]
    public async Task<ActionResult> AddNewConference(ConferenceCreateDto conferenceCreateDto)
    {
        var conference = await _service.AddNewConference(conferenceCreateDto);
        
        var result = new
        {
            Id = conference.Id,
            conferenceCreateDto
        };
        
        return Ok(result);
    }

    [HttpPut("updateConference")]
    public async Task<ActionResult> UpdateConference(ConferenceDto confDto)
    {
        var updateConf = await _service.UpdateConference(confDto);
        
        return Ok(updateConf);
    }

    [HttpPost("addNewConferenceWithImg")]
    public async Task<ActionResult<Conference>> AddNewConferenceWithImg([FromForm]ConferenceWithImgDto conferenceWithImgDto)
    {
        var fullPath = $"{_configuration["PathConferencePhoto"]}\\{conferenceWithImgDto.Img.FileName}";
        var fileStream = new FileStream(fullPath, FileMode.Create);
        await conferenceWithImgDto.Img.CopyToAsync(fileStream);
        
        /*var conference = await _service.AddNewConference(conferenceWithImgDto.ConferenceDto);
        var photo = await _photosService.AddNewConferencePhoto(conference.Id,conferenceWithImgDto.Img.FileName);

        var imgUrl = $"api/photos/getConferencePhotoById{conference.PhotoId}";

        var result = new
        {
            conference, imgUrl
        };*/
        return Ok();
       
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

    [HttpDelete("DeleteConference")]
    public async Task<ActionResult> DeleteConference(Guid confId)
    {
        var deleteConf = await _service.DeleteConference(confId);

        return Ok(deleteConf);
    }
}