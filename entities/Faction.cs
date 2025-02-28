using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AbstractGame.entities
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


        //for debuggging
        public static void TestJSON(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: JSON file not found.");
                return;
            }

            string json = File.ReadAllText(filePath);
            FactionData factions = JsonSerializer.Deserialize<FactionData>(json);

            if (factions == null || factions.Factions == null)
            {
                Console.WriteLine("Error: Failed to deserialize JSON.");
                return;
            }

            Console.WriteLine("=== Faction Data ===");
            foreach (var faction in factions.Factions)
            {
                Console.WriteLine($"\nFaction: {faction.Key}");
                Console.WriteLine($"Description: {faction.Value.description}");

                Console.WriteLine("Playstyles:");
                foreach (var playstyle in faction.Value.Playstyles)
                {
                    Console.WriteLine($"  - {playstyle.Key}: {playstyle.Value}");
                }
            }
        }
        
    }
}
