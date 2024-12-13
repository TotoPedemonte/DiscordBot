using System;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class PlayerDied
{
    public static void OnPlayerDied(DiedEventArgs ev)
    {
        var userId = ev.Player.UserId;
        if (EventsHandler.timeStats.ContainsKey(userId))
        {
            var stats = EventsHandler.timeStats[userId];
            var timeAlive = DateTime.UtcNow - stats.lastJoinTime.Date;
            if (timeAlive > stats.longestTimeAlive)
            {
                stats.longestTimeAlive = timeAlive;
            }
        }
    }
}