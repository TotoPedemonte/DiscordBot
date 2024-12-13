using System;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class PlayerLeft
{
    public static void LeftServer(LeftEventArgs ev)
    {
        var userId = ev.Player.UserId;
        
        if (EventsHandler.timeStats.ContainsKey(userId))
        {
            var stats = EventsHandler.timeStats[userId];
            stats.lastSeen = DateTime.UtcNow;
            stats.timeSpent += DateTime.UtcNow - stats.lastJoinTime.Date;
            stats.lastJoinTime = DateTime.Now;
        }
    }
}