using Discord_Bot.Modules;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class Killing
{
    public static void KillingPlayerEv(DiedEventArgs ev)
    {
        if (ev.Attacker != null && ev.Attacker != ev.Player && !ev.DamageHandler.IsSuicide)
        {
            var attackerId = ev.Attacker.UserId;
            
            if (!EventsHandler.killsStats.ContainsKey(attackerId))
            {
                EventsHandler.killsStats[attackerId] = new KillsStats
                {
                    Nickname = ev.Attacker.Nickname
                };
            }
            
            var killStats = EventsHandler.killsStats[attackerId];

            if (ev.Player.Role.Side == Side.Scp)
                killStats.scpKills++;
            else if (ev.Player.Role.Side == Side.Tutorial)
                return;
            else killStats.humanKills++;
        }
    }
}