using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.QuestionnaireDTOs;
using Microsoft.AspNetCore.Mvc;

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
}