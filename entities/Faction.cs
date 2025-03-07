using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AbstractGame
{
    public class Faction
    {
        public string description { get; set; }
        public Dictionary<string, string> Playstyles { get; set; }
    }
    public class FactionData
    {
        public Dictionary<string, Faction> Factions { get; set; }
    }
    public class FactionLoader
    {
        public static FactionData LoadFactions(string filePath)
        {
              string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<FactionData>(json);
        }
    }
}
