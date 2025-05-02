using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using AbstractGame.systems;
using Newtonsoft.Json;

namespace AbstractGame.entities
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
        public float[] QualityRange { get; set; }
        public float[] AvailabilityRange { get; set; }
        public Dictionary<string, float> WeaponTypes { get; set; }
        public List<string> WeaponTags { get; set; }
        public float WeightLimit { get; set; }
        public int[] SpawnCount { get; set; }
        public string ProbabilityWeighting { get; set; }

        public List<string> GearTags { get; set; }
        public int GearMaxItems { get; set; }

        public List<string> ItemTags { get; set; }
        public int ItemMax { get; set; }
    }

    public class ItemManager
    {


        /*public Dictionary<string, object> CreateInventory(string faction, string playstyle, string difficulty) //start with a stub
        {
            var inventory = new Dictionary<string, object>();

            //Load rule config for faction/playstyle
            //Adjust quality range based on difficulty
            //Query eligible items from DB (filtered by tags/types/quality)
            //Prioritize item categories (to circumbvent weight problem) //start with weapon, then gear/clothing, then essentials (food water) then misc (nav, comms, tools)
            //Check total weight limit while adding
            //For each added item include dynamic data (if needed) (ammo, fuel, etc.)

            JSONDeserializer.T LoadJson<T>(); //watch out here

            return inventory;
        }*/

        public class InventoryManager
        {
            public static ItemFilters LoadItemRules(string faction, string playstyle)
            {
                string json = System.IO.File.ReadAllText(@"C:\path\to\your\spawnRules.json");
                var root = JsonConvert.DeserializeObject<InventoryRulesRoot>(json);

                //Loading faction/playstyle rules
                return root.InvItemRules[faction][playstyle].ItemFilters;  // This line is fine now

            }

            public static List<WeaponItem> GetEligibleWeapons(string faction, string playstyle, int difficulty)
            {
                // Load the spawn rules for the given faction and playstyle
                var itemFilters = LoadItemRules(faction, playstyle);

                // Adjust quality range based on difficulty
                int minQuality = (int)(itemFilters.QualityRange[0] + (difficulty - 1));  //error CS1061
                int maxQuality = (int)(itemFilters.QualityRange[1] - (difficulty - 1));  //error CS1061

                // Prepare the SQL query with parameters
                string allowedTypes = string.Join(",", itemFilters.WeaponTypes.Keys); //error CS1061
                string allowedTags = string.Join(",", itemFilters.WeaponTags); //error CS1061

                string query = @"
                SELECT wpnName, wpnType, quality, availability, 'Gun' AS Source
                FROM stat_wpn_gun
                WHERE quality BETWEEN @MinQuality AND @MaxQuality
                  AND wpnType IN (@AllowedTypes)
                  AND EXISTS (
                    SELECT 1
                    FROM STRING_SPLIT(tags, ',')
                    WHERE value IN (@AllowedTags)
                  )
                UNION ALL
                SELECT wpnName, wpnType, quality, availability, 'Melee' AS Source
                FROM stat_wpn_melee
                WHERE quality BETWEEN @MinQuality AND @MaxQuality
                  AND wpnType IN (@AllowedTypes)
                  AND EXISTS (
                    SELECT 1
                    FROM STRING_SPLIT(tags, ',')
                    WHERE value IN (@AllowedTags)
                  )
                UNION ALL
                SELECT wpnName, wpnType, quality, availability, 'Launcher' AS Source
                FROM stat_wpn_launcher
                WHERE quality BETWEEN @MinQuality AND @MaxQuality
                  AND wpnType IN (@AllowedTypes)
                  AND EXISTS (
                    SELECT 1
                    FROM STRING_SPLIT(tags, ',')
                    WHERE value IN (@AllowedTags)
                  )
                UNION ALL
                SELECT wpnName, wpnType, quality, availability, 'Ordnance' AS Source
                FROM stat_wpn_ordnance
                WHERE quality BETWEEN @MinQuality AND @MaxQuality
                  AND wpnType IN (@AllowedTypes)
                  AND EXISTS (
                    SELECT 1
                    FROM STRING_SPLIT(tags, ',')
                    WHERE value IN (@AllowedTags)
                  );";

                // Execute the query and return the result
                List<WeaponItem> weapons = new List<WeaponItem>();

                using (var connection = new SQLiteConnection(DBManager.connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@MinQuality", minQuality);
                        command.Parameters.AddWithValue("@MaxQuality", maxQuality);
                        command.Parameters.AddWithValue("@AllowedTypes", allowedTypes);
                        command.Parameters.AddWithValue("@AllowedTags", allowedTags);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var weapon = new WeaponItem
                                {
                                    Name = reader["wpnName"].ToString(),
                                    Type = reader["wpnType"].ToString(),
                                    Quality = Convert.ToInt32(reader["quality"]),
                                    Availability = Convert.ToInt32(reader["availability"]),
                                    Source = reader["Source"].ToString()
                                };
                                weapons.Add(weapon);
                            }
                        }
                    }
                }
                return weapons;
            }

            public static Dictionary<string, object> GenerateInventory(string faction, string playstyle, int difficulty)
            {
                // Load the eligible weapons based on the faction, playstyle, and difficulty
                var eligibleWeapons = GetEligibleWeapons(faction, playstyle, difficulty);

                // We will assume some inventory structure here
                var inventory = new Dictionary<string, object>();

                // Prioritize weapon, gear, essential items
                List<WeaponItem> mainWeapon = new List<WeaponItem>();
                List<string> gear = new List<string>();

                // Select the main weapon (priority: first weapon in eligible list)
                if (eligibleWeapons.Count > 0)
                {
                    mainWeapon.Add(eligibleWeapons[0]);  // First weapon, for example
                    inventory["MainWeapon"] = mainWeapon;
                }

                // Add other items like gear
                gear.Add("Jacket");  // Example gear item
                gear.Add("Boots");   // Example gear item
                inventory["Gear"] = gear;

                // Add essentials (water, food, etc.)
                List<string> essentials = new List<string> { "Water", "Food" };
                inventory["Essentials"] = essentials;

                return inventory;
            }
        }

        public class WeaponItem
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Quality { get; set; }
            public int Availability { get; set; }
            public string Source { get; set; }
        }


        public static class InventoryRuleLoader //
        {
            public static InventoryRulesRoot LoadInventoryRules(string filePath)
            {
                string json = File.ReadAllText(filePath);
                var rules = JsonConvert.DeserializeObject<InventoryRulesRoot>(json);
                return rules ?? new InventoryRulesRoot();
            }
        }



        public double GetQualityChance(int quality) //experimental
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
