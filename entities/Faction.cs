﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AbstractGame
{
    //GameOPtions.json
    public class Faction //migrate all to JSONManager.cs?
    {
        public string description { get; set; }
        public Dictionary<string, string> Playstyles { get; set; }
    }
    public class FactionData
    {
        public Dictionary<string, Faction> Factions { get; set; }
    }

    //InvConfig.json
    public class PlaystyleCondition  // Represents a single playstyle's properties
    {
        public List<int> AllowedQualities { get; set; }
        public List<string> Weapons { get; set; }
        public int ArmorLevel { get; set; }
    }

    public class InventoryConfig  // Represents the root JSON object
    {
        public Dictionary<string, Dictionary<string, PlaystyleCondition>> Factions { get; set; }
        public Dictionary<string, float> DifficultyModifiers { get; set; }
    }

    public class JSONLoader //nicht modular, only works for GameOptions.json, I have to somehow make the return object modular?? (JSONObject and make the method type dynamic //guess what its <T> (generic type that can be set to anything)
    {
        public static FactionData LoadFactions(string filePath)
        {
              string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<FactionData>(json);
        }
    }
    public class JSONDeserializer //move modular version over to JSONManager.cs
    {
        public static T LoadJson<T>(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex) {Console.WriteLine("JSON file failed to deserlialize:"); throw; }
            
        }

        public static void JSONSelect()
        {
            var inventoryConfig = JSONDeserializer.LoadJson<InventoryConfig>(@"C:\Users\Neo\source\repos\AbstractGame\AbstractGame\resources\InvConfig.json");

            // Example: Get the "Raider" playstyle from the "Bandit" faction
            var raider = inventoryConfig.Factions["Bandit"]["Raider"];

            Console.WriteLine($"Raider's armor level: {raider.ArmorLevel}");

        }
    }

    


   
}
