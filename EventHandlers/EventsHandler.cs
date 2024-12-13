using System.Collections.Generic;
using Discord_Bot.Modules;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Discord_Bot.EventHandlers;

public class EventsHandler
{
    public static Dictionary<string, PlayerStats> playerStats = new();
    public static Dictionary<string, KillsStats> killsStats = new();
    public static Dictionary<string, TimeStats> timeStats = new();
    
    public static void RegisterEvents()
    {
        Exiled.Events.Handlers.Player.Died += Player.Killing.KillingPlayerEv;
        Exiled.Events.Handlers.Player.UsedItem += Player.ItemUse.UsingItem;
        Exiled.Events.Handlers.Player.Escaped += Player.Escaping.PlayerEscaped;
        Exiled.Events.Handlers.Player.Hurt += Player.DamageDealt.DealingDamage;
        Exiled.Events.Handlers.Player.Handcuffing += Player.Cuffing.PlayerCuffed;
        Exiled.Events.Handlers.Player.DroppedItem += dropItemToSave;
        
        Exiled.Events.Handlers.Player.Died += Player.PlayerDied.OnPlayerDied;
        Exiled.Events.Handlers.Player.Left += Player.PlayerLeft.LeftServer;
        Exiled.Events.Handlers.Player.Verified += Player.PlayerVerified.Verified;
        
        Exiled.Events.Handlers.Server.WaitingForPlayers += Server.WaitingPly.WaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundEnded += Server.RoundFinished.RoundFinish;
    }
    public static void UnRegisterEvents()
    {
        Exiled.Events.Handlers.Player.Died -= Player.Killing.KillingPlayerEv;
        Exiled.Events.Handlers.Player.UsedItem -= Player.ItemUse.UsingItem;
        Exiled.Events.Handlers.Player.Escaped -= Player.Escaping.PlayerEscaped;
        Exiled.Events.Handlers.Player.Hurt -= Player.DamageDealt.DealingDamage;
        Exiled.Events.Handlers.Player.Handcuffing -= Player.Cuffing.PlayerCuffed;
        Exiled.Events.Handlers.Player.DroppedItem -= dropItemToSave;
        
        Exiled.Events.Handlers.Player.Died -= Player.PlayerDied.OnPlayerDied;
        Exiled.Events.Handlers.Player.Left -= Player.PlayerLeft.LeftServer;
        Exiled.Events.Handlers.Player.Verified -= Player.PlayerVerified.Verified;
        
        Exiled.Events.Handlers.Server.WaitingForPlayers -= Server.WaitingPly.WaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundEnded -= Server.RoundFinished.RoundFinish;
    }

    private static void dropItemToSave(DroppedItemEventArgs ev)
    {
        if (ev.Player.IsAlive && ev.Pickup.Category == ItemCategory.Grenade)
        {
            DataManagment.SaveData();
            Log.Warn("Saving data from weapon");
        }
    }
}