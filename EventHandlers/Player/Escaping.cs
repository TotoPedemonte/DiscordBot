using Discord_Bot.Modules;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class Escaping
{
    public static void PlayerEscaped(EscapedEventArgs ev)
    {
        if (ev.Player != null)
        {
            var escapedPly = ev.Player.UserId;
            
            if (!EventsHandler.playerStats.ContainsKey(escapedPly))
            {
                EventsHandler.playerStats[escapedPly] = new PlayerStats
                {
                    Nickname = ev.Player.Nickname
                };
            }
            
            var escapedStats = EventsHandler.playerStats[escapedPly];
            escapedStats.Escapes++;
        }
    }
}