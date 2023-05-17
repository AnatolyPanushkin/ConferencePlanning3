using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.QuestionnaireDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;


[Route("api/questionnaire")]
[ApiController]
public class QuestionnaireController:ControllerBase
{
    
    private readonly ConferencePlanningContext _context;

    public QuestionnaireController(ConferencePlanningContext context)
    {
        _context = context;
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


    