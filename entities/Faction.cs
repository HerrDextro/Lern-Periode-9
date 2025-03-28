using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AbstractGame
{
    //GameOPtions.json
    public class Faction
    {
        public string description { get; set; }
        public Dictionary<string, string> Playstyles { get; set; }
    }
    public class FactionData
    {
        public Dictionary<string, Faction> Factions { get; set; }
    }

    //InvConfig.json
    public class PlaystyleInventoryRelation
    {
        public List<int> AllowedQualities { get; set; } //list bc no keys needed, just like an array
        public List<string> Weapons { get; set; }
        public int ArmorLevel { get; set; }
    }
    public class InventoryConditions
    {
        public Dictionary<string, PlaystyleInventoryRelation> FactionsPlaystyles { get; set; }
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
    public class JSONDeserializer
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
    }


   
}
