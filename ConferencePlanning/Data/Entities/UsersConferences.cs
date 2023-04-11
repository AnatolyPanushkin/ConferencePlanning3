namespace ConferencePlanning.Data.Entities;

public class UsersConferences
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public Guid ConferenceId { get; set; }
    public Conference Conference { get; set; }
}