using System;
using Discord_Bot.Modules;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class PlayerVerified
{
    public static void Verified(VerifiedEventArgs ev)
    {
        var userId = ev.Player.UserId;
        if (!EventsHandler.timeStats.ContainsKey(userId))
        {
            EventsHandler.timeStats[userId] = new TimeStats
            {
                firstJoin = DateTime.UtcNow,
                lastSeen = DateTime.UtcNow,
                timeSpent = TimeSpan.Zero,
                longestTimeAlive = TimeSpan.Zero,
                roundsPlayed = 0,
                Nickname = ev.Player.Nickname
            };
        }

        EventsHandler.timeStats[userId].lastJoinTime = DateTime.UtcNow;
    }
}