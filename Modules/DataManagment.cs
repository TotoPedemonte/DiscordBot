using System;
using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace Discord_Bot.Modules;

public abstract class DataManagment
{
    private static readonly string ConfigDirectory = Main.Instance.Config.JsonDirectory;
    private static readonly string plyFileName = Main.Instance.Config.plyJsonFileName;
    private static readonly string killFileName = Main.Instance.Config.killsJsonFileName;
    private static readonly string timeFileName = Main.Instance.Config.timeJsonFileName;
    
    private static readonly string plyFilePath = Path.Combine(ConfigDirectory, plyFileName);
    private static readonly string killFilePath = Path.Combine(ConfigDirectory, killFileName);
    private static readonly string timeFilePath = Path.Combine(ConfigDirectory, timeFileName);

    private static bool dataSaved;

    
    public static void SaveData()
    {
        try
        {
            if (!dataSaved)
            {
                if (!Directory.Exists(ConfigDirectory))
                {
                    Directory.CreateDirectory(ConfigDirectory);
                }

                Dictionary<string, PlayerStats> existingPlayerData = new Dictionary<string, PlayerStats>();
                Dictionary<string, KillsStats> existingKillsData = new Dictionary<string, KillsStats>();
                Dictionary<string, TimeStats> existingTimeData = new Dictionary<string, TimeStats>();

                /*PLAYER FILE*/
                
                if (File.Exists(plyFilePath))
                {
                    string existingJson = File.ReadAllText(timeFilePath);
                    existingPlayerData = JsonConvert.DeserializeObject<Dictionary<string, PlayerStats>>(existingJson) ?? new Dictionary<string, PlayerStats>();
                }
                
                foreach (var entry in EventHandlers.EventsHandler.playerStats)
                {
                    existingPlayerData[entry.Key] = entry.Value;
                }
                
                string plyJson = JsonConvert.SerializeObject(existingPlayerData, Formatting.Indented);
                File.WriteAllText(plyFilePath, plyJson);
                
                /*KILLS FILE*/
                
                if (File.Exists(killFilePath))
                {
                    string existingJson = File.ReadAllText(killFilePath);
                    existingKillsData = JsonConvert.DeserializeObject<Dictionary<string, KillsStats>>(existingJson) ?? new Dictionary<string, KillsStats>();
                }
                
                foreach (var entry in EventHandlers.EventsHandler.killsStats)
                {
                    existingKillsData[entry.Key] = entry.Value;
                }
                
                string killsJson = JsonConvert.SerializeObject(existingKillsData, Formatting.Indented);
                File.WriteAllText(killFilePath, killsJson);
                
                /*TIME FILE*/
                
                if (File.Exists(timeFilePath))
                {
                    string existingJson = File.ReadAllText(timeFilePath);
                    existingTimeData = JsonConvert.DeserializeObject<Dictionary<string, TimeStats>>(existingJson) ?? new Dictionary<string, TimeStats>();
                }
                
                foreach (var entry in EventHandlers.EventsHandler.timeStats)
                {
                    existingTimeData[entry.Key] = entry.Value;
                }
                
                string timeJson = JsonConvert.SerializeObject(existingTimeData, Formatting.Indented);
                File.WriteAllText(timeFilePath, timeJson);
                
                dataSaved = true;
                
                Log.Warn("Data saved in Json Files!");
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error while saving data: {ex.Message}");
        }
    }
    
    public static Dictionary<string, PlayerStats> LoadPlayerStats()
    {
        try
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
            if (File.Exists(plyFilePath))
            {
                string jsonData = File.ReadAllText(plyFilePath);
                dataSaved = false;
                return JsonConvert.DeserializeObject<Dictionary<string, PlayerStats>>(jsonData) ?? new Dictionary<string, PlayerStats>();
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error while loading data: {ex.Message}");
        }

        return new Dictionary<string, PlayerStats>();
    }
    
    public static Dictionary<string, KillsStats> LoadKillStats()
    {
        try
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
            if (File.Exists(killFilePath))
            {
                string jsonData = File.ReadAllText(killFilePath);
                dataSaved = false;
                return JsonConvert.DeserializeObject<Dictionary<string, KillsStats>>(jsonData) ?? new Dictionary<string, KillsStats>();
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error while loading data: {ex.Message}");
        }

        return new Dictionary<string, KillsStats>();
    }
    
    public static Dictionary<string, TimeStats> LoadTimeStats()
    {
        try
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
            if (File.Exists(timeFilePath))
            {
                string jsonData = File.ReadAllText(timeFilePath);
                dataSaved = false;
                return JsonConvert.DeserializeObject<Dictionary<string, TimeStats>>(jsonData) ?? new Dictionary<string, TimeStats>();
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error while loading data: {ex.Message}");
        }

        return new Dictionary<string, TimeStats>();
    }
}