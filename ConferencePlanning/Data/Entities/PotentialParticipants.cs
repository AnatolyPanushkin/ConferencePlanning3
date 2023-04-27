namespace ConferencePlanning.Data.Entities;

public class PotentialParticipants
{
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public Guid ConferenceId { get; set; }
    public virtual Conference Conference { get; set; }
}