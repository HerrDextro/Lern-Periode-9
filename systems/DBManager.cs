using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AbstractGame.systems
{
    public class DBManager
    {
        //private static string folderPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "resources"); // Folder path ChatGPT magic, doesnt rlly work so well (always went into bin/debug folder)
        private static string folderPath = @"C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources"; //hard path for now until I figure out how this works (no time)
        private static string dbPath;
        public static string connectionString;

        /*public static SQLiteConnection OpenConnection()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }*/

        public static void SetGameDatabase(string gameName)
        {
            dbPath = Path.Combine(folderPath, $"{gameName}.db");
            connectionString = $"Data Source={dbPath};Version=3;";

            Console.WriteLine($"Database set to: {dbPath}");
        }
        public static void CreateDB(string dbName)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                Console.WriteLine("ERROR: Database path is not set. Call SetGameDatabase() first."); //debug
            }

            try
            {
                SQLiteConnection.CreateFile($"{dbName}.db");
                Console.WriteLine($"DB created successfully at: {dbPath}");
            }
            catch
            {
                Console.WriteLine("DB could not be created");
            }
            
        }
        public static void InitDB()
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                Console.WriteLine("ERROR: Database path is not set. Call SetGameDatabase() first."); //debug
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTablesQuery = @"
            CREATE TABLE IF NOT EXISTS Player (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Faction TEXT NOT NULL,
            Playstyle TEXT NOT NULL,
            Money INTEGER DEFAULT 0,
            Health INTEGER DEFAULT 100,
            Difficulty INT NOT NULL);";

                using (var command = new SQLiteCommand(createTablesQuery, connection))
                {
                    Console.WriteLine("InitDB Lines affected: " + command.ExecuteNonQuery());
                }
            }
        }
        public static void DBInsert(SQLiteConnection connection, string tableName, Dictionary<string, object> values) //little param confusion
        {
            string columns = string.Join(", ", values.Keys);
            string parameters = string.Join(", ", values.Keys.Select(k => $"@{k}"));

            string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                foreach (var kvp in values)
                {
                    command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
                }
                Console.WriteLine("DBInsert Lines affected:" + command.ExecuteNonQuery());
            }


        }
        public static void QueryDB(string query)
        {
            using (var connection = new SQLiteConnection(connectionString)) //only declare connection here not in later methods. (remove "var")
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    Console.WriteLine("InitDB Lines affected:" + command.ExecuteNonQuery());
                }
            }
        }
    }
}
