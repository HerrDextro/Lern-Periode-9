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
using Newtonsoft.Json;

namespace AbstractGame.entities
{
    public class InventoryRulesRoot
    {
        public Dictionary<string, Dictionary<string, InvItemRule>> InvItemRules { get; set; }
    }

    public class InvItemRule
    {
        public float[] QualityRange { get; set; }       
        public float[] AvailabilityRange { get; set; }  
        public ItemFilters ItemFilters { get; set; }
    }

    public class ItemFilters
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

        public Dictionary<string, object> CreateInventory(string rulesPath, string faction, string playStyle, int difficulty) //difficulty is int, right? check
        {
            // get all the rules 
            InvItemRule rule = InvJSONdeserializer(rulesPath, faction, playStyle);

            int minQuality;
            int maxQuality;
            int maxAvailability;
            int minAvailability;
            Dictionary<string, object> itmFilter = new Dictionary<string, object>(); //critical, check this later
            string[] wpnTags;
            float weightLimit;
            int[] maxMinWpnSpawn;
            string probabilityWeighting; //NOTE, this is for the following: "distribution": select randomly based on the type weights. "equal": ignore weights, pick anything."fixed": always pick a fixed setup.
            string[] gearTags;
            int maxGearItms;
            string itmTags;
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
            wpnTags = rule.ItemFilters.WeaponTags.ToArray(); //works? somehow.
            weightLimit = rule.ItemFilters.WeightLimit;
            maxMinWpnSpawn= rule.ItemFilters.SpawnCount.ToArray();
            probabilityWeighting = rule.ItemFilters.ProbabilityWeighting.ToString(); //cannot convert to char issue. //WATCH OUT: IF THERE ARE MORE THAN 1 Entries this BREAKS
            gearTags = rule.ItemFilters.GearTags.ToArray();
            maxGearItms = Convert.ToInt32(rule.ItemFilters.GearMaxItems);
            itmTags = rule.ItemFilters.ItemTags.ToString(); //WATCH OUT: IF THERE ARE MORE THAN 1 Entries this BREAKS
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



            // get all items (+ insert rest of filters into query)




            




            return new Dictionary<string, object>(); //to avoid constant "not returning error"
        }

        public static InvItemRule InvJSONdeserializer(string path, string faction, string playStyle)
        {
            string json = File.ReadAllText(path);

            var root = JsonConvert.DeserializeObject<InventoryRulesRoot>(json); //newtonsoft, check older deserializer methods (might wanna update them)

            var rule = root.InvItemRules[faction][playStyle];
            Console.WriteLine(rule); //debug, onyl returns object type tho
            
            PrintAllSpawnRules(root);//
            return rule;
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
