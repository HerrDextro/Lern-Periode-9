using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using AbstractGame.systems;
using System.Resources;
using AbstractGame.entities;

namespace AbstractGame.debug
{
    public class Debug
    {
        //static string dbPath = "gameSave.db";
        //public static string connectionString = $"Data Source={dbPath};Version=3;"; //here was the issue

        public static void Query()
        {
            Console.WriteLine("select JSONSelect,SQLQuery, SQLCreateDB or \"SQLSelectQuery\" or 5 to test a method");
            
            bool exit = false;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "JSONSelect":
                    Console.WriteLine("Input fileName:");
                    string fileName = Console.ReadLine();
                    Debug.TestJSON($@"C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\{fileName}.json"); //change to absolute
                    break;
                case "SQLQuery":
                    Console.WriteLine("Choose DB name, then table to select * from:");
                    string dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    DBManager.SQLDebugQuery(Console.ReadLine());
                    break;
                case "SQLSelectQuery":
                    Console.WriteLine("Choose DB name, then insert query. NOTE: large queries are problematic in cmd");
                    dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    Console.WriteLine("Input your SQL query here");
                    string sqlQuery = Console.ReadLine();
                    DBManager.SQLSelectQuery(sqlQuery); 
                    break;
                case "SQLCreateDB":
                    Console.WriteLine("input new DB name");
                    dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    DBManager.CreateDB(dbName); //still crêates the DB
                    break;
                case "5":
                    //JSONDeserializer.JSONSelect();
                    //ItemManager.CreateInventory("Factionless", "Light", "1"); //last 1 int to string issue look into it
                    //ItemManager.InvJSONdeserializer("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\TEST.json", "Factionless", "Light");
                    //ItemManager.PrintAllSpawnRules(ItemManager.root);
                    DBManager.SetDatabasePath("StaticRefDB");

                    //DBManager.QueryDB("INSERT INTO test_wpn_gun \r\n(wpnName, wpnTag, ammoType, magSize, rpm, durability, compatibility, weight, quality, availability) \r\nVALUES \r\n('Tokarev TT-33', 'light-reliable', '7.62x25mm Tokarev', 8, 600, 75, 'None', 0.85, 3, 9),\r\n('SKS', 'medium-reliable', '7.62x39mm', 10, 400, 100, 'None', 3.8, 3, 9),\r\n('AK-47', 'medium-reliable', '7.62x39mm', 30, 600, 100, 'None', 3.3, 4, 10),\r\n('HK G3', 'heavy-powerful', '7.62x51mm NATO', 20, 600, 100, 'None', 4.1, 6, 5),\r\n('SCAR H', 'heavy-powerful', '7.62x51mm NATO', 20, 650, 95, 'None', 3.9, 8, 2);\r\n"); //works
                    ItemManager.CreateInventory("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\TEST.json", "Factionless", "Light", 1);
                    Console.ReadLine();
                    //DBManager.QueryDB(ItemManager.wpnQuery);
                    break;
            }
        }

        

        public static void TestJSON(string filePath) //just like JSONLoader, gotta make it modular
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: JSON file not found.");
                return;
            }

            string json = File.ReadAllText(filePath);
            FactionData factions = JsonSerializer.Deserialize<FactionData>(json);

            //making modular
            string json2 = File.ReadAllText(filePath);
            InventoryConfig factions2 = JSONDeserializer.LoadJson<InventoryConfig>(json2);


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
        public class Experiment //see la ter
        {
            public Dictionary<string, string> Value { get; set; }
        }
        
    }
     
}
