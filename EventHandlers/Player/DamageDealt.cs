using System;
using Exiled.Events.EventArgs.Player;
using Discord_Bot.Modules;
using Exiled.API.Features;

namespace Discord_Bot.EventHandlers.Player;

public abstract class DamageDealt
{
    public static void DealingDamage(HurtEventArgs ev)
    {
        if (ev.Attacker != null && ev.Player != null && !ev.DamageHandler.IsSuicide && ev.Attacker.IsAlive && ev.Attacker != ev.Player)
        {
            
            var attackerID = ev.Attacker.UserId;
            
            if (!EventsHandler.playerStats.ContainsKey(attackerID))
            {
                EventsHandler.playerStats[attackerID] = new PlayerStats
                {
                    Nickname = ev.Attacker.Nickname
                };
            }
            
            var damageStats = EventsHandler.playerStats[attackerID];
            damageStats.DamageDealt += (int)Math.Round(ev.Amount);
        }
    }
}