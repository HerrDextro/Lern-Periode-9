using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using AbstractGame.systems;
using AbstractGame.world;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AbstractGame.entities
{
    public class InventoryRulesRoot //JSON
    {
        public Dictionary<string, Dictionary<string, InvItemRule>> InvItemRules { get; set; }
    }

    public class InvItemRule //JSON
    {
        public float[] QualityRange { get; set; }       
        public float[] AvailabilityRange { get; set; }  
        public ItemFilters ItemFilters { get; set; }
    }

    public class ItemFilters //JSON
    {
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
    public class InvEntry //gameItem reference, bot the item yet (otherwise the list would have to support objects that could contain any if the SQL "objects"
    {

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

        //public static string wpnQuery; //for Debug.cs to access it. Belongs to createInventory
        public static Dictionary<string, object> CreateInventory(string rulesPath, string faction, string playStyle, int difficulty) //difficulty is int, right? check
        {
            // get all the rules 
            InvItemRule rule = InvJSONdeserializer(rulesPath, faction, playStyle);

            int minQuality;
            int maxQuality;
            int maxAvailability;
            int minAvailability;
            Dictionary<string, object> itmFilter = new Dictionary<string, object>(); //critical, check this later
            string[] wpnTags;
            float maxWeight;
            int[] maxMinWpnSpawn;
            string probabilityWeighting; //NOTE, this is for the following: "distribution": select randomly based on the type weights. "equal": ignore weights, pick anything."fixed": always pick a fixed setup.
            string[] gearTags;
            int maxGearItms;
            string[] itmTags;
            int maxItms;

            
            minQuality = (int)rule.QualityRange[0];
            maxQuality = (int)rule.QualityRange[1];
            minAvailability = (int)rule.QualityRange[0];
            maxAvailability = (int)rule.QualityRange[1];
            //ignored the dctionary for now
            foreach (var wpn in itmFilter.Keys)
            {
                //dsaid ignored dictionary
            }
            wpnTags = rule.ItemFilters.WeaponTags.ToArray(); //works? somehow. Apparfently it doesnt. The SQL select gives exception because System.String[] is interted into the query
            maxWeight = rule.ItemFilters.WeightLimit;
            maxMinWpnSpawn= rule.ItemFilters.SpawnCount.ToArray();
            probabilityWeighting = rule.ItemFilters.ProbabilityWeighting.ToString(); //cannot convert to char issue. //WATCH OUT: IF THERE ARE MORE THAN 1 Entries this BREAKS
            gearTags = rule.ItemFilters.GearTags.ToArray();
            maxGearItms = Convert.ToInt32(rule.ItemFilters.GearMaxItems);
            itmTags = rule.ItemFilters.ItemTags.ToArray(); //WATCH OUT: IF THERE ARE MORE THAN 1 Entries this BREAKS
            maxItms = Convert.ToInt32(rule.ItemFilters.ItemMax);




            // Apply  modifiers (other ones are implemented straight into the SQL query as variables, for example "Tag")
            //first quality with difficulty
            switch(difficulty)
            {
                case 1:
                    //general quality trend should be: higher quality
                    minQuality += 1;
                    break;
                case 2:
                    //this shouldnt change anything about the quality (linear dificulty from 1 to 3 for now)
                    break;
                case 3:
                    //general qaulity trend should be: lower quality
                    if(minQuality > 0)
                    {
                        minQuality -= 1;
                    }
                    maxQuality -= 1;
                    break;
            }



            // get all items (+ insert rest of filters into query) //only test_wpn_table for now!!!
            //better make this a function
            //string  wpnQuery = $"SELECT * FROM test_wpn_gun \r\nWHERE quality BETWEEN {minQuality} AND {maxQuality}\r\n  AND availability BETWEEN {minAvailability} AND {maxAvailability}\r\n  AND weight <= {maxWeight}\r\n  AND wpnTag LIKE {wpnTags};\r\n";
            string wpnQuery = BuildQuery(wpnTags, minQuality, maxQuality, minAvailability, maxAvailability, maxWeight);
            //DBManager.SQLSelectQuery(wpnQuery);
            Console.WriteLine(wpnTags[0] + "," + wpnTags[1]);


            //DEBUG CHECKING LIST OF RETUNRED ITEMS:
            var items = DBManager.SQLSelectQuery("SELECT * FROM test_wpn_gun"); //normally uses wpnQuery, but for testing uses select 
            if (items.Count > 0)
            {
                foreach (var row in items)
                {
                    Console.WriteLine("Row:");
                    foreach (var pair in row)
                    {
                        Console.WriteLine($"  {pair.Key}: {pair.Value}"); 
                    }
                }
            }
            else 
            {
                Console.WriteLine("No items found");
            }

            //NOW CREATE LOADOUT BASED ON RETURNED ITEMS
            //highest priority: weapons
            //then gear (clothing, backpack)
            //then food and water
            //then items (comms, tools)
            //then medical
            var pistols = new List<Item>();
            var rifles = new List<Item>();
            var melee = new List<Item>();
            var smoke = new List<Item>();
            // Gear, Food, Medical, Tools, etc.

            List<WpnGun> weapons = items.Select(ConvertRowToWeapon).ToList();

            foreach (var weapon in weapons)
            {
                if (weapon.Type == "Pistol") pistols.Add(weapon);
                else if (weapon.Type == "SemiAuto") rifles.Add(weapon);
                else if (weapon.Type == "Melee") melee.Add(weapon);
            }


            //Only wpns now for testing, expand for medical and gear


            return new Dictionary<string, object>(); //to avoid constant "not returning error"
        }
        public static WpnGun ConvertRowToWeapon(Dictionary<string, object> row)
        {
            return new WpnGun
            {
                Id = Convert.ToInt32(row["stat_wpn_gun_id"]),
                Name = row["wpnName"].ToString(),
                Tag = row["wpnTag"].ToString(),
                Type = row["wpnType"].ToString(), // assuming this field exists
                AmmoType = row["ammoType"].ToString(),
                MagSize = Convert.ToInt32(row["magSize"]),
                RPM = Convert.ToInt32(row["rpm"]),
                Durability = string.IsNullOrWhiteSpace(row["durability"].ToString()) ? 0 : Convert.ToInt32(row["durability"]),
                DurabilityMod = string.IsNullOrWhiteSpace(row["durabilityMod"].ToString()) ? 0 : Convert.ToInt32(row["durabilityMod"]),
                Compatibility = row["compatibility"].ToString(),
                Weight = Convert.ToSingle(row["weight"]),
                Quality = Convert.ToInt32(row["quality"]),
                Availability = Convert.ToInt32(row["availability"]),
                Tags = row["wpnTag"].ToString().Split(',').ToList()
            };
        }



        public static InvItemRule InvJSONdeserializer(string path, string faction, string playStyle)
        {
            string json = File.ReadAllText(path);

            var root = JsonConvert.DeserializeObject<InventoryRulesRoot>(json); //newtonsoft, check older deserializer methods (might wanna update them)

            var rule = root.InvItemRules[faction][playStyle];
            Console.WriteLine("InvJSONdeserializer: (Debug)" + rule); //debug, onyl returns object type tho
            
            PrintAllSpawnRules(root);//
            return rule;
        }
        public static string BuildQuery(string[] tags, int minQuality, int maxQuality, int minAvail, int maxAvail, double maxWeight)
        {
            var likeClauses = tags.Select(tag => $"wpnTag LIKE '%{tag}%'");
            var whereTags = string.Join(" OR ", likeClauses);

            var query = $@"
            SELECT * FROM test_wpn_gun 
             WHERE quality BETWEEN {minQuality} AND {maxQuality}
            AND availability BETWEEN {minAvail} AND {maxAvail}
            AND weight <= {maxWeight}
            AND ({whereTags});
    ";

            return query;
        }



        //this below is either for getting the final items into an inventory list or it was for doing the actual select with a return
        public List<InvEntry> GetItems(float minQuality, float maxQuality, float minAvailability, float maxAvailability, float maxWeight, string requiredTag) //no idea why this is here and what its for, so delete if not touched until next week.
        {
            


            List<InvEntry> inventory = new List<InvEntry>(); //to solve return error

            return inventory;
        }

        public static void PrintAllSpawnRules(InventoryRulesRoot root) //ChatGPT for debug
        {
            foreach (var faction in root.InvItemRules)
            {
                Console.WriteLine($"Faction: {faction.Key}");

                foreach (var playstyle in faction.Value)
                {
                    Console.WriteLine($"\tPlaystyle: {playstyle.Key}");

                    var rule = playstyle.Value;

                    Console.WriteLine($"\t\tQuality Range: {rule.QualityRange[0]} to {rule.QualityRange[1]}");
                    Console.WriteLine($"\t\tAvailability Range: {rule.AvailabilityRange[0]} to {rule.AvailabilityRange[1]}");

                    var filters = rule.ItemFilters;

                    Console.WriteLine($"\t\tWeight Limit: {filters.WeightLimit}");

                    Console.WriteLine("\t\tWeapon Types:");
                    foreach (var wpn in filters.WeaponTypes)
                        Console.WriteLine($"\t\t\t{wpn.Key}: {wpn.Value}");

                    Console.WriteLine("\t\tWeapon Tags: " + string.Join(", ", filters.WeaponTags));
                    Console.WriteLine("\t\tGear Tags: " + string.Join(", ", filters.GearTags));
                    Console.WriteLine("\t\tItem Tags: " + string.Join(", ", filters.ItemTags));

                    Console.WriteLine($"\t\tMax Gear Items: {filters.GearMaxItems}");
                    Console.WriteLine($"\t\tMax Item Count: {filters.ItemMax}");
                    Console.WriteLine($"\t\tProbability Weighting: {filters.ProbabilityWeighting}");
                }
            }
        }
    }
}
