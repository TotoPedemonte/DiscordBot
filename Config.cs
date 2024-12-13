using Exiled.API.Interfaces;

namespace Discord_Bot
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        public ulong GuildId { get; set; } = 1316826045407559730;
    }
}