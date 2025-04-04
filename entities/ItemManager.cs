using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame.entities
{
    public class ItemManager
    {
        public static void CreateInventory(string faction, string playstyle, int difficulty)
        {
            var inventoryConfig = JSONDeserializer.LoadJson<InventoryConfig>(@"C:\Users\Neo\source\repos\AbstractGame\AbstractGame\resources\InvConfig.json");
            var configuration = inventoryConfig.Factions[faction][playstyle];
            Console.WriteLine($"player allowed qualities : {configuration.AllowedQualities[0]}"); //seee what w^to do about the index

            // Example: Get the "Raider" playstyle from the "Bandit" faction
            var raider = inventoryConfig.Factions["Bandit"]["Raider"];

            Console.WriteLine($"Raider's armor level: {raider.ArmorLevel}");
        }

        public float GetQualityChance(int quality)
        {
            // Define base spawn chances per quality level (example values)
            return quality switch
            {
                1 => 0.9f,
                2 => 0.7f,
                3 => 0.5f,
                4 => 0.3f,
                5 => 0.1f,
                _ => 0f, //no idea what this one does yet
            };
        }
    }
}
