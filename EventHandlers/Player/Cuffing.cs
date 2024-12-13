using Exiled.Events.EventArgs.Player;
using Discord_Bot.Modules;

namespace Discord_Bot.EventHandlers.Player;

public abstract class Cuffing
{
    public static void PlayerCuffed(HandcuffingEventArgs ev)
    {
        if (ev.IsAllowed && ev.Player != null & ev.Target != null) 
        {
            var cufferId = ev.Player.UserId;

            if (!EventsHandler.playerStats.ContainsKey(cufferId))
            {
                EventsHandler.playerStats[cufferId] = new PlayerStats();
                EventsHandler.playerStats[cufferId].Nickname = ev.Player.Nickname;
            }
            
            var cuffStats = EventsHandler.playerStats[cufferId];
            cuffStats.PlayersCuffed += 1;
        }
    }
}