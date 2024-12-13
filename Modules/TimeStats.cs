using System;

namespace Discord_Bot.Modules;

public class TimeStats
{
    public DateTime firstJoin { get; set; }
    public DateTime lastSeen { get; set; }
    public TimeSpan timeSpent { get; set; }
    public TimeSpan longestTimeAlive { get; set; }
    public DateTime lastJoinTime { get; set; }
    public int roundsPlayed { get; set; } = 0;
    public string Nickname { get; set; }
}