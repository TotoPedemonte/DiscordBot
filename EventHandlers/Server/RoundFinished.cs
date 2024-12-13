using Discord_Bot.Modules;
using Exiled.Events.EventArgs.Server;

namespace Discord_Bot.EventHandlers.Server;

public class RoundFinished
{
    public static void RoundFinish(RoundEndedEventArgs ev)
    {
        foreach (var player in Exiled.API.Features.Player.List)
        {
            var userId = player.UserId;
            if (EventsHandler.timeStats.ContainsKey(userId))
            {
                EventsHandler.timeStats[userId].roundsPlayed++;
            }
        }
        DataManagment.SaveData();
    }
}