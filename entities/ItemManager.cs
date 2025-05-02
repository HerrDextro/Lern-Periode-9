using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame.entities
{
    public class ItemManager
    {
        public class InventoryRulesRoot //from now on the base is called root //CHANGE IN OTHER JSON
        {
            public Dictionary<string, Dictionary<string, InvItemRule>> InvItemRules { get; set; }
        }
        public class InvItemRule
        {
            public float[] QualityRange { get; set; } //same as before, but now use graph value
            public float[] AvailabilityRange { get; set; }
            public ItemFilters ItemFilters { get; set; }
        }
        public class ItemFilters
        {
            public Dictionary<string, float> WeaponTypes { get; set; }  //see SQL comment under main Table structure
            public List<string> WeaponTags { get; set; }
            public float WeightLimit { get; set; } //only relevant for lighter loadout (had scout and light in mind)
            public int[] SpawnCount { get; set; }  // Min and max number of weapons
            public string ProbabilityWeighting { get; set; }

            public List<string> GearTags { get; set; }
            public int GearMaxItems { get; set; }

            public List<string> ItemTags { get; set; }
            public int ItemMax { get; set; }
        }

        public static void CreateInventory(string faction, string playstyle, int difficulty)
        {
            var inventoryConfig = JSONDeserializer.LoadJson<InventoryConfig>(@"C:\Users\Neo\source\repos\AbstractGame\AbstractGame\resources\InvConfig.json");
            var configuration = inventoryConfig.Factions[faction][playstyle];
            Console.WriteLine($"player allowed qualities : {configuration.AllowedQualities[0]}"); //seee what w^to do about the index

            // Example: Get the "Raider" playstyle from the "Bandit" faction
            var raider = inventoryConfig.Factions["Bandit"]["Raider"];
            Console.WriteLine($"Raider's armor level: {raider.ArmorLevel}"); //ok now we know the whole query thing works

            //load allowed quality, weapontypes and armorlevel
            //create inventory sketch based on the allowed type ->how many items of what kind (1x rilfe, 2x cloth, 3x medical, 4x ammo, 1x tool)
            //assign qualitylevel to each item  based on allowed qualities, and their probability based on the faction and playstyle mix (faction gives base probability, playsty^le gives extra and fixed qualities (commander has 5 gun)
            //extact all table entires that match the kind of item (for example gun type rifle and use the mix and max value of the id column to generate a random pick) //maybe we should also have familiar weapons for specific factions, eg SCAR is elites favorite or so

            int[] allowedQualities = configuration.AllowedQualities.ToArray();
            string[] weaponTypes = configuration.Weapons.ToArray();
            var armorLevel = configuration.ArmorLevel;

            //as for the new JSON Structure, what do we need?
            //NEW PLAN: for the player a new faction called "selection" that results in a larger "Inventory generated" so the player can manually pick his/hers fav items
            //we restructure the JSON so that quality levels are with weapons and gear

            var result = new List<string>();
            var random = new Random();

            // Just get random weapons
            var weapons = Console.ReadLine();
                .Where(item => item.Type == "Pistol" || item.Type == "Rifle")
                .OrderBy(x => random.Next())
                .Take(2)
                .ToList();

            result.AddRange(weapons.Select(x => x.Name));

            // Random gear (navigation or survival)
            var gear = allItems
                .Where(item => item.Tags.Contains("Navigation") || item.Tags.Contains("SurvivalBasic"))
                .OrderBy(x => random.Next())
                .Take(3)
                .ToList();

            result.AddRange(gear.Select(x => x.Name));
        }

        public double GetQualityChance(int quality)
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
