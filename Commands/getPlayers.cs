using System;
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
            Name = Main.Instance.Config.Commands.parentCommand,
            Description = Main.Instance.Config.Commands.parentCommandDescription,
            DefaultMemberPermissions = GuildPermission.Administrator,
            Options = new()
            {
                new()
                {
                    //Medical Items
                    Name = Main.Instance.Config.Commands.SubCommands.medicalCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.medicalCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.KillsCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.KillsCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommandGroup,
                    Options = new()
                    {
                        new ()
                        {
                            Name = Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.CommandName,
                            Description = Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.Description,
                            IsRequired = false,
                            Type = ApplicationCommandOptionType.SubCommand,
                        },
                        new ()
                        {
                            Name = Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.CommandName,
                            Description = Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.Description,
                            IsRequired = false,
                            Type = ApplicationCommandOptionType.SubCommand,
                        }
                    }
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.escapesCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.escapesCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.cuffsCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.cuffsCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.damageCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.damageCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.roundCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.roundCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.timespentCommand.CommandName,
                    Description = Main.Instance.Config.Commands.SubCommands.timespentCommand.Description,
                    IsRequired = false,
                    Type = ApplicationCommandOptionType.SubCommand
                },
                new()
                {
                    Name = Main.Instance.Config.Commands.SubCommands.timealiveCommand.CommandName,
                    Description =  Main.Instance.Config.Commands.SubCommands.timealiveCommand.Description,
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
            
            if (subcommand == Main.Instance.Config.Commands.SubCommands.medicalCommand.CommandName)
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.MedicalItems)
                    .Take(Main.Instance.Config.Commands.SubCommands.medicalCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.medicalCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.medicalCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.medicalCommand.Body, index + 1, p.Value.Nickname, p.Value.MedicalItems))) + "```"); /*$"\u001b[2;33m\u001b[2;33m{index + 1, 2}.\u001b[0m\u001b[2;33m\u001b[0m {p.Value.Nickname,-20} Medical Items: {p.Value.MedicalItems, -2}"*/
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.medicalCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.KillsCommand.CommandName)
            {
                string subsubcommand = command.Data.Options.First().Options.First().Name;
                if (subsubcommand == Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.CommandName)
                {
                    var topPlayers = EventsHandler.killsStats.OrderByDescending(p => p.Value.humanKills)
                        .Take(Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.AmmountOfPlyToDisplay)
                        .ToList();
                    EmbedBuilder embed = new EmbedBuilder()
                        .WithTitle(Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.Title)
                        .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.Color, NumberStyles.HexNumber))
                        .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.Body, index + 1, p.Value.Nickname, p.Value.humanKills))) + "```");
                    await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.KillsCommand.humanKills.PrivateMessage, embed:embed.Build());
                    return;
                }
                if (subsubcommand == Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.CommandName)
                {
                    var topPlayers = EventsHandler.killsStats.OrderByDescending(p => p.Value.scpKills)
                        .Take(Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.AmmountOfPlyToDisplay)
                        .ToList();
                    EmbedBuilder embed = new EmbedBuilder()
                        .WithTitle(Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.Title)
                        .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.Color, NumberStyles.HexNumber))
                        .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.Body, index + 1, p.Value.Nickname, p.Value.scpKills))) + "```");                    
                    await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.KillsCommand.scpKills.PrivateMessage, embed:embed.Build());
                }
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.escapesCommand.CommandName)
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.Escapes)
                    .Take(Main.Instance.Config.Commands.SubCommands.escapesCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.escapesCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.escapesCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.escapesCommand.Body, index + 1, p.Value.Nickname, p.Value.Escapes))) + "```");                    
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.escapesCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.cuffsCommand.CommandName)
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.PlayersCuffed)
                    .Take(Main.Instance.Config.Commands.SubCommands.cuffsCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.cuffsCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.cuffsCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.cuffsCommand.Body, index + 1, p.Value.Nickname, p.Value.PlayersCuffed))) + "```");                    
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.cuffsCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.damageCommand.CommandName)
            {
                var topPlayers = EventsHandler.playerStats.OrderByDescending(p => p.Value.DamageDealt)
                    .Take(Main.Instance.Config.Commands.SubCommands.damageCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.damageCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.damageCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.damageCommand.Body, index + 1, p.Value.Nickname, p.Value.DamageDealt))) + "```");                    
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.damageCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.roundCommand.CommandName)
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.roundsPlayed)
                    .Take(Main.Instance.Config.Commands.SubCommands.roundCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.roundCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.roundCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.roundCommand.Body, index + 1, p.Value.Nickname, p.Value.roundsPlayed))) + "```");                    
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.roundCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.timespentCommand.CommandName)
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.longestTimeAlive)
                    .Take(Main.Instance.Config.Commands.SubCommands.timespentCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.timespentCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.timespentCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.timespentCommand.Body, index + 1, p.Value.Nickname, p.Value.timeSpent.Hours, p.Value.timeSpent.Minutes, p.Value.timeSpent.Seconds))) + "```");
                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.timespentCommand.PrivateMessage, embed:embed.Build());
                return;
            }

            if (subcommand == Main.Instance.Config.Commands.SubCommands.timealiveCommand.CommandName)
            {
                var topPlayers = EventsHandler.timeStats.OrderByDescending(p => p.Value.timeSpent)
                    .Take(Main.Instance.Config.Commands.SubCommands.timealiveCommand.AmmountOfPlyToDisplay)
                    .ToList();
                EmbedBuilder embed = new EmbedBuilder()
                    .WithTitle(Main.Instance.Config.Commands.SubCommands.timealiveCommand.Title)
                    .WithColor(uint.Parse(Main.Instance.Config.Commands.SubCommands.timealiveCommand.Color, NumberStyles.HexNumber))
                    .WithDescription("```ansi\n" + string.Join("\n", topPlayers.Select((p, index) => String.Format(Main.Instance.Config.Commands.SubCommands.timealiveCommand.Body, index + 1, p.Value.Nickname, p.Value.longestTimeAlive.Hours, p.Value.longestTimeAlive.Minutes, p.Value.longestTimeAlive.Seconds))) + "```");                await command.RespondAsync("", ephemeral:Main.Instance.Config.Commands.SubCommands.timealiveCommand.PrivateMessage, embed:embed.Build());
            }
        }
    }
}