using ConferencePlanning.DTO;
using Microsoft.AspNetCore.SignalR;

namespace ConferencePlanning.Hubs;

public class ConferenceHub:Hub
{
    public async Task NotifyUser(string user, string message)
    {
        await Clients.All.SendAsync("DisplayNotification", user, message);
    }
}