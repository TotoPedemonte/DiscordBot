using System.ComponentModel;
using System.IO;
using Exiled.API.Interfaces;

namespace Discord_Bot
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        public ulong GuildId { get; set; } = 0;
        
        [Description("Data and Json Managment")]
        public string JsonDirectory { get; set; } = Path.Combine("C:\\", "Users", "USER", "AppData", "Roaming", "EXILED", "Configs", "DiscordBotStats");
        public string plyJsonFileName { get; set; } = "playerStats.json";
        public string killsJsonFileName { get; set; } = "killStats.json";
        public string timeJsonFileName { get; set; } = "timeStats.json";

        [Description("\nDiscord Bot Configuration\n")]
        public CommandsConfig Commands { get; set; } = new CommandsConfig();

        public class CommandsConfig
        {
            [Description("Parent command from the Discord Bot Plugin")]
            public string parentCommand { get; set; } = "leaderboard";
            
            [Description("Parent command description")]
            public string parentCommandDescription { get; set; } = "Parent command from the Discord Bot Plugin";
            
            [Description("Configurations for the subcommands")]
            public SubCommand SubCommands { get; set; } = new();
        }
        public class SubCommand
        {
            [Description("{0}: Index number (1.) - {1}: Player Nickname - {2}: Value")]
            public medicalCommandConfig medicalCommand { get; set; } = new();
            public killsCommandConfig KillsCommand { get; set; } = new();
            public escapesCommandConfig escapesCommand { get; set; } = new();
            public cuffsCommandConfig cuffsCommand { get; set; } = new();
            public damageCommandConfig damageCommand { get; set; } = new();
            public roundCommandConfig roundCommand { get; set; } = new();
            public timespentCommandConfig timespentCommand { get; set; } = new();
            public timealiveCommandConfig timealiveCommand { get; set; } = new();
        }
        
        public class medicalCommandConfig
        {
            public string CommandName { get; set; } = "medical";
            public string Description { get; set; } = "Get the top 10 Players with most medical items used";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Medical Item Uses**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m {0, 2}. \u001b[0m\u001b[2;33m\u001b[0m {1, -20} Medical Items: {2, -2} ";
            public string Color { get; set; } = "3498DB";
            public bool PrivateMessage { get; set; } = false;
        }
        public class killsCommandConfig
        {
            public string CommandName { get; set; } = "kills";
            public string Description { get; set; } = "Get the top 10 Players with most SCP or Human kills";
            
            public subKillsCommand scpKills { get; set; } = new()
            {
                CommandName = "scp",
                Description = "Get the top 10 Players with most SCP kills",
                
                Title = "**Top 10 Players With most SCP Kills**",
                Body = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1,-20} SCP Kills: {2, -2}",
                Color = "3498DB",
                PrivateMessage = false
            };
            public subKillsCommand humanKills { get; set; } = new()
            {
                CommandName = "human",
                Description = "Get the top 10 Players with most human kills",
                
                Title = "**Top 10 Players With most Human Kills**",
                Body = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1,-20} Human Kills: {2, -2}",
                Color = "3498DB",
                PrivateMessage = false
            };
        }
        public class subKillsCommand
        {
            public string CommandName { get; set; }
            public string Description { get; set; }
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; }
            public string Body { get; set; }
            public string Color { get; set; }
            public bool PrivateMessage { get; set; }
        }
        public class escapesCommandConfig
        {
            public string CommandName { get; set; } = "escapes";
            public string Description { get; set; } = "Get the top 10 Players with most facility escapes";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Escapes**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Escapes: {2, -2}";
            public string Color { get; set; } = "E6F562";
            public bool PrivateMessage { get; set; } = false;
        }
        public class cuffsCommandConfig
        {
            public string CommandName { get; set; } = "cuffs";
            public string Description { get; set; } = "Get the top 10 Players with most pleyers cuffed";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Players Cuffed**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Players Cuffed: {2, -2}";
            public string Color { get; set; } = "919191";
            public bool PrivateMessage { get; set; } = false;
        }
        public class damageCommandConfig
        {
            public string CommandName { get; set; } = "damage";
            public string Description { get; set; } = "Get the top 10 Players with most damage dealt";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Damage Dealt**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Total Damage Dealt: {2, -2}";
            public string Color { get; set; } = "520800";
            public bool PrivateMessage { get; set; } = false;
        }
        public class roundCommandConfig
        {
            public string CommandName { get; set; } = "rounds";
            public string Description { get; set; } = "Get the top 10 Players with most rounds played";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Rounds Played**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Rounds Played: {2, -2}";
            public string Color { get; set; } = "068A3D";
            public bool PrivateMessage { get; set; } = false;
        }
        public class timespentCommandConfig
        {
            public string CommandName { get; set; } = "timespent";
            public string Description { get; set; } = "Get the top 10 Players with most time spent in-game";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Players with Most Time Spent**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Time Spent: {2}h {3}m {4}s";
            public string Color { get; set; } = "068A3D";
            public bool PrivateMessage { get; set; } = false;
        }
        public class timealiveCommandConfig
        {
            public string CommandName { get; set; } = "timealive";
            public string Description { get; set; } = "Get the top 10 Players with most time beeing alive";
            public int AmmountOfPlyToDisplay { get; set; } = 10;
            
            [Description("Information About the Embed Message to upload")]
            public string Title { get; set; } = "**Top 10 Most Time Alive**";
            public string Body { get; set; } = "\u001b[2;33m\u001b[2;33m{0, 2}.\u001b[0m\u001b[2;33m\u001b[0m {1, -20} Time Alive: {2}h {3}m {4}s";
            public string Color { get; set; } = "068A3D";
            public bool PrivateMessage { get; set; } = false;
        }
    }
}