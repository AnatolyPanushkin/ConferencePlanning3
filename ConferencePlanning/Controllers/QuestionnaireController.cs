using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.QuestionnaireDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;


[Route("api/questionnaire")]
[ApiController]
public class QuestionnaireController:ControllerBase
{
    
    private readonly ConferencePlanningContext _context;
    private const string SIGNALR_HUB_URL = "http://localhost:5215/hub";
    private static HubConnection hub;
    
    
    public QuestionnaireController(ConferencePlanningContext context)
    {
        _context = context;
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        hub.StartAsync();
    }

    [HttpGet("getQuestionnaireByUserId")]
    public async Task<ActionResult> GetQuestionnaireByUserId(string userId)
    {
        var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(q => q.UserId.Equals(userId));

        if (questionnaire!=null)
        {
            return Ok(questionnaire);
        }

        return BadRequest("Questionnaire is nor exist");
    }

    [HttpPost("AddNewQuestionnaire")]
    public async Task<ActionResult> AddNewQuestionnaire(QuestionnaireDto questionnaireDto)
    {
        var questionnaire = new Questionnaire
        {
            Id = Guid.NewGuid(),
            DockladTheme = questionnaireDto.DockladTheme,
            ScientificDegree = questionnaireDto.ScientificDegree,
            Type = questionnaireDto.Type,
            UserId = questionnaireDto.UserId
        };

        _context.Questionnaires.Add(questionnaire);
        
        _context.PotentialParticipants.Add(new() { UserId = questionnaire.UserId, ConferenceId = questionnaireDto.ConferenceId });
        await _context.SaveChangesAsync();
        
        return Ok(questionnaire);
    }

    [HttpPost("changeStatus")]
    public async Task<ActionResult> ChangeStatus(Guid quesId, string status)
    {
       
        var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(q => q.Id == quesId);

        if (questionnaire!=null)
        {
            if (status.Equals("Accepted"))
            {
                questionnaire.Status = StatusValue.Accepted;
                await hub.SendAsync("NotifyUser", "Conference_Notifier", StatusValue.Accepted.ToString());
            }
            else if (status.Equals("NotAccepted"))
            {
                questionnaire.Status = StatusValue.NotAccepted;
            }
            else
            {
                return BadRequest("Incorrect value of status");
            }
            
            _context.Questionnaires.Update(questionnaire);
        }
        
        await _context.SaveChangesAsync();

        return Ok("Status was changed");
    }

    [HttpDelete("deletePotentialParticipant")]
    public async Task<ActionResult> DeletePotentialParticipant(string userId)
    {
        var potentialParticipant =
            await _context.PotentialParticipants.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

        if (potentialParticipant!=null)
        {
           var deleteParticipant =
               _context.PotentialParticipants.Remove(potentialParticipant).Entity;

           await _context.SaveChangesAsync();
           
           return Ok(deleteParticipant);
        }

        return BadRequest("Potential participant with this Id is not exist");
    }
    
    
}


    