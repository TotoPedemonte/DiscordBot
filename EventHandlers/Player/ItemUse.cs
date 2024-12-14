using Discord_Bot.Modules;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers.Player;

public abstract class ItemUse
{
    public static void UsingItem(UsedItemEventArgs ev)
    {
        if (ev.Player != null && ev.Item.Category == ItemCategory.Medical)
        {
            if (!EventsHandler.playerStats.ContainsKey(ev.Player.UserId))
            {
                EventsHandler.playerStats[ev.Player.UserId] = new PlayerStats
                {
                    Nickname = ev.Player.Nickname
                };
            }
            
            var itemStats = EventsHandler.playerStats[ev.Player.UserId];
            itemStats.MedicalItems++;
        }
    }
}