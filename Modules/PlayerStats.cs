namespace Discord_Bot.Modules;

public class PlayerStats
{
    public int MedicalItems { get; set; } = 0;
    public int PlayersCuffed { get; set; } = 0;
    public int Escapes { get; set; } = 0;
    public int DamageDealt { get; set; } = 0;
    public string Nickname { get; set; }
}