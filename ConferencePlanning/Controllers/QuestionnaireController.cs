using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
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
            var potentialParticipants = await _context.PotentialParticipants.Include(pp=>pp.Conference).FirstOrDefaultAsync(
                pp => pp.UserId.Equals(questionnaire.UserId));
            
            if (status.Equals("Accepted"))
            {
                //Change status of questionnaire
                questionnaire.Status = StatusValue.Accepted;
                
                _context.Questionnaires.Update(questionnaire);
                
                //Send notification
                var message = $"{questionnaire.Status.ToString()}" +
                                  $"{potentialParticipants.Conference.Name}" +
                                  $"{potentialParticipants.Conference.Date.ToString()}" +
                                  $"{questionnaire.Type}";
                
                await hub.SendAsync("NotifyUser", "Conference_Notifier", message);
                
                //add user into list of participants
                _context.UsersConferences.Add(new() 
                   { 
                       UserId = potentialParticipants.UserId,
                       ConferenceId = potentialParticipants.ConferenceId
                   });
                
                //remove user from list of potential participants
                _context.PotentialParticipants.Remove(potentialParticipants);

                await _context.SaveChangesAsync();
            }
            
            else if (status.Equals("NotAccepted"))
            {
                questionnaire.Status = StatusValue.NotAccepted;
                
                _context.Questionnaires.Update(questionnaire);
                await _context.SaveChangesAsync();
            }
            
            else
            {
                return BadRequest("Incorrect value of status");
            }
            
            
            return Ok("Status was changed");
        }

        return BadRequest("Questionnaire is not exist");
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


    