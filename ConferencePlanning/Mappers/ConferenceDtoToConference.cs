using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO.ConferenceDto;


namespace ConferencePlanning.Mappers;

public static class ConferenceDtoToConference
{
    public static Conference MapConferenceDtoToConference(this ConferenceDto conferenceDto)
    {
        var conference = new Conference
        {
            Name = conferenceDto.Name,
            ShortTopic = conferenceDto.ShortTopic,
            FullTopic = conferenceDto.FullTopic,
            Addres = conferenceDto.Addres,
            City = conferenceDto.City,
            Type = conferenceDto.Type,
            Date = conferenceDto.Date,
            StartTime = conferenceDto.StartTime,
            EndTime = conferenceDto.EndTime,
            //Organizer = conferenceDto.Organizer,
            ModeratorId = conferenceDto.ModeratorId,
            Categories = conferenceDto.Categories
        };

        return conference;
    }
}