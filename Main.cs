using System;
using Discord_Bot.EventHandlers;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace Discord_Bot
{
    public class Main : Plugin<Config>
    {
        public override string Name => "Discord Leaderboard Bot";
        public override string Prefix => "Discord Bot";
        public override string Author => "Totoped157";
        public override PluginPriority Priority { get; } = PluginPriority.Default;
        public override Version Version { get; } = new(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new(9, 0, 0); 
        
        public static Main Instance { get; private set; }
        private HandlerLoader _handlerLoader;

        public override void OnEnabled()
        {
            Instance = this;
            _handlerLoader = new ();
            _handlerLoader.Load(Assembly);
            
            EventsHandler.RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            _handlerLoader.Unload();
            _handlerLoader = null;
            
            EventsHandler.UnRegisterEvents();
            
            base.OnDisabled();
        }
    }
}