using Discord_Bot.Modules;

namespace Discord_Bot.EventHandlers.Server;

public class WaitingPly
{
    public static void WaitingForPlayers()
    {
        EventsHandler.playerStats = DataManagment.LoadPlayerStats();
        EventsHandler.killsStats = DataManagment.LoadKillStats();
        EventsHandler.timeStats = DataManagment.LoadTimeStats();
    }
}