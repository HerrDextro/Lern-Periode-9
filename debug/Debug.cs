using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame.debug
{
    public class Debug
    {
        static string dbPath = "gameSave.db";
        public static string connectionString = $"Data Source={dbPath};Version=3;";


        public static void SelectAll(string tableName)
        {
            using (var connection = new SQLiteConnection(connectionString))
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
