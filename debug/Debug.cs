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
            Console.WriteLine("select JSONSelect,SQLSelect, SQLCreateDB or \"SQLQuery\" or 5 to test a method");
            
            bool exit = false;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "JSONSelect":
                    Console.WriteLine("Input fileName:");
                    string fileName = Console.ReadLine();
                    Debug.TestJSON($@"C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\{fileName}.json"); //change to absolute
                    break;
                case "SQLSelect":
                    Console.WriteLine("Choose DB name, then table to select * from:");
                    string dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    Debug.SelectAll(Console.ReadLine());
                    break;
                case "SQLQuery":
                    Console.WriteLine("Choose DB name, then table to select * from:");
                    dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    Console.WriteLine("Input your SQL query here");
                    string sqlQuery = Console.ReadLine();
                    DBManager.QueryDB(sqlQuery); 
                    break;
                case "SQLCreateDB":
                    Console.WriteLine("input new DB name");
                    dbName = Console.ReadLine();
                    DBManager.SetDatabasePath(dbName);
                    DBManager.CreateDB(dbName); //still crêates the DB
                    break;
                case "5":
                    //JSONDeserializer.JSONSelect();
                    ItemManager.CreateInventory("Factionless", "Light", 1);
                    break;
            }
        }

        public static void SelectAll(string tableName)
        {
            try
            {
                using (var connection = new SQLiteConnection(DBManager.connectionString))
                {
                    connection.Open();
                    string selectAllQuery = $@"SELECT * FROM {tableName}";

                    using (var command = new SQLiteCommand(selectAllQuery, connection))
                    using (var reader = command.ExecuteReader()) // Execute the query
                    {
                        while (reader.Read()) // Loop through all rows
                        {
                            for (int i = 0; i < reader.FieldCount; i++) // Loop through all columns
                            {
                                Console.Write($"{reader.GetName(i)}: {reader[i]} | \n"); // Print column name & value
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write ($"Debug selectAll: {ex.Message}");
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
