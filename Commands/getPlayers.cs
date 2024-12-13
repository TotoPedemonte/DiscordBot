using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord_Bot.EventHandlers;
using Discord;
using Discord.WebSocket;
using DiscordLab.Bot.API.Interfaces;
using Exiled.API.Features;

namespace Discord_Bot.Commands
{
    public class getPlayers : ISlashCommand
    {
        public SlashCommandBuilder Data { get; } = new()
        {
            Name = "leaderboard",
            Description = "obtener los players de la partida",
            DefaultMemberPermissions = GuildPermission.Administrator,
            Options = new()
            {
                new()
                {
                    Name = "medical",
                    Description = "Get the top 10 Players with most medical items used",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "kills",
                    Description = "Get the top 10 Players with most medical items used",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommandGroup,
                    Options = new()
                    {
                        new ()
                        {
                            Name = "human",
                            Description = "Get the top 10 Players with most human kills",
                            IsRequired = false,
                            Type = ApplicationCommandOptionType.SubCommand,
                        },
                        new ()
                        {
                            Name = "scp",
                            Description = "Get the top 10 Players with most scp kills",
                            IsRequired = false,
                            Type = ApplicationCommandOptionType.SubCommand,
                        }
                    }
                },
                new()
                {
                    Name = "escapes",
                    Description = "Get the top 10 Players with most facility escapes",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "cuffs",
                    Description = "Get the top 10 Players with most pleyers cuffed",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "damage",
                    Description = "Get the top 10 Players with most damage dealt",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "rounds",
                    Description = "Get the top 10 Players with most rounds played",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "timespent",
                    Description = "Get the top 10 Players with most time spent in-game",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = "timealive",
                    Description = "Get the top 10 Players with most time beeing alive",
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
            }
        };
        
        public ulong GuildId { get; set; } = Main.Instance.Config.GuildId;

        public async Task Run(SocketSlashCommand command)
        {
            if (Round.IsEnded)
            {
                await command.RespondAsync("Error, Round is not in progress!", ephemeral:true);
                return;
            }
            
            string subcommand = command.Data.Options.First().Name;
            
            if (subcommand == "medical")
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.MedicalItems)
                    .Take(10)
                    .ToList();
                /*
                string response = "## **Top 10 Most Medical Item Uses**\n" + "```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Medical Items Used: {p.Value.MedicalItems, -2}")) + "```";
                */
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Medical Item Uses**")
                    .WithColor(uint.Parse("3498DB", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Medical Items: {p.Value.MedicalItems, -2}")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "kills")
            {
                string subsubcommand = command.Data.Options.First().Options.First().Name;
                if (subsubcommand == "human")
                {
                    var topPlayers = EventsHandler.killsStats.OrderByDescending(p => p.Value.humanKills)
                        .Take(10)
                        .ToList();
                    EmbedBuilder embed = new EmbedBuilder()
                        .WithTitle("**Top 10 Most human kills**")
                        .WithColor(uint.Parse("01540F", NumberStyles.HexNumber))
                        .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Kills: {p.Value.humanKills, -2}")) + "```");
                    await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                    return;
                }
                else
                {
                    var topPlayers = EventsHandler.killsStats.OrderByDescending(p => p.Value.scpKills)
                        .Take(10)
                        .ToList();
                    EmbedBuilder embed = new EmbedBuilder()
                        .WithTitle("**Top 10 Most SCP kills**")
                        .WithColor(uint.Parse("D60000", NumberStyles.HexNumber))
                        .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Kills: {p.Value.scpKills, -2}")) + "```");
                    await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                }
            }

            if (subcommand == "escapes")
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.Escapes)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Escapes**")
                    .WithColor(uint.Parse("E6F562", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Escapes: {p.Value.Escapes, -2}")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "cuffs")
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.PlayersCuffed)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Players Cuffed**")
                    .WithColor(uint.Parse("919191", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Players Cuffed: {p.Value.PlayersCuffed, -2}")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "damage")
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.DamageDealt)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Damage Dealt**")
                    .WithColor(uint.Parse("520800", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Total Damage Dealt: {p.Value.DamageDealt, -2}")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "rounds")
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.roundsPlayed)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Rounds Played**")
                    .WithColor(uint.Parse("068a3d", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Rounds Played: {p.Value.roundsPlayed, -2}")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "timespent")
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.longestTimeAlive)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Time Alive**")
                    .WithColor(uint.Parse("068a3d", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Time Alive: {p.Value.longestTimeAlive.Hours}h {p.Value.longestTimeAlive.Minutes}m {p.Value.longestTimeAlive.Seconds}s")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }

            if (subcommand == "timealive")
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.timeSpent)
                    .Take(10)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle("**Top 10 Most Time Played**")
                    .WithColor(uint.Parse("068a3d", NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => $"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Time Played: {p.Value.timeSpent.Hours}h {p.Value.timeSpent.Minutes}m {p.Value.timeSpent.Seconds}s")) + "```");
                await command.RespondAsync("", ephemeral:false, embed:embed.Build());
                return;
            }
        }
    }
}