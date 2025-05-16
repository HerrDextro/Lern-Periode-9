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
        private static string folderPath = @"C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\"; //hard path for now until I figure out how this works (no time)
        private static string dbPath;
        public static string connectionString;

        /*public static SQLiteConnection OpenConnection()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }*/

        public static void SetDatabasePath(string gameName)
        {
            //dbPath = Path.Combine(folderPath, $"{gameName}.db");
            //connectionString = $"Data Source={dbPath};Version=3;"; 
            connectionString = $"Data Source=C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\{gameName}.db;Version=3;";

            Console.WriteLine($"Database path set to: {connectionString}");
        }
        public static void CreateDB(string dbName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("ERROR: Database path is not set. Call SetGameDatabase() first."); //debug //keeps triggering for no apparent reason, atleast 2 times or 3 times
            }

            try
            {
                SQLiteConnection.CreateFile($"{dbName}.db");
                Console.WriteLine($"DB created successfully at: {connectionString}");
            }
            catch
            {
                Console.WriteLine("DB could not be created");
            }
            InitDB(); //moved here for SQL logic error no such table as Player
            
        }
        public static void InitDB()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("ERROR: Database path is not set. Call SetGameDatabase() first."); //debug
            }

            
            
            string createTablesQuery2 = @"
            CREATE TABLE IF NOT EXISTS Player (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Faction TEXT NOT NULL,
            Playstyle TEXT NOT NULL,
            Money INTEGER DEFAULT 0,
            Health INTEGER DEFAULT 100,
            Difficulty INT NOT NULL);

            CREATE TABLE test_wpn_gun (
            stat_wpn_gun_id INTEGER PRIMARY KEY AUTOINCREMENT,
            wpnName TEXT NOT NULL,
            wpnTag TEXT NOT NULL,
            ammoType TEXT,
            magSize INTEGER,
            rpm INTEGER,
            durability INTEGER,
            durabilityMod INTEGER,
            compatibility TEXT,
            weight REAL,
            quality REAL NOT NULL,
            availability REAL NOT NULL);";

            try 
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();

                using var command = new SQLiteCommand(createTablesQuery2, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'Player' created successfully.");
            }
            catch(SQLiteException ex)
            {
                Console.WriteLine($"creating tables failed at: + {ex.Message}");
            }
            /*using (var connection = new SQLiteConnection(connectionString))
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
            }*/

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
        public static void SQLInsertQuery(string query) //only creates and inserts/updates, no selects or anything with a return
        {
            using (var connection = new SQLiteConnection(connectionString)) //only declare connection here not in later methods. (remove "var")
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    Console.WriteLine("QueryDB Lines affected:" + command.ExecuteNonQuery());
                }
            }
        }
        public static List<Dictionary<string, object>> SQLSelectQuery(string query)
        {
            Console.WriteLine("SQLSelectQuery CALLED");

            var results = new List<Dictionary<string, object>>();

            try
            {
                using (var connection = new SQLiteConnection(DBManager.connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.GetValue(i);
                                row[columnName] = value;
                            }

                            results.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database query failed: {ex.Message}");
                // Optionally rethrow or return empty list
                throw;
            }

            return results;
        }
        /*public static void SelectAll(string tableName) //debug
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
                Console.Write($"Debug selectAll: {ex.Message}");
            }

        }*/
        public static void SQLDebugQuery(string Query) //debug
        {
            try
            {
                using (var connection = new SQLiteConnection(DBManager.connectionString))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(Query, connection))
                    using (var reader = command.ExecuteReader()) // Execute the query
                    {
                        while (reader.Read()) // Loop through all rows
                        {
                            for (int i = 0; i < reader.FieldCount; i++) // Loop through all columns
                            {
                                Console.Write($"{reader.GetName(i)}: {reader[i]} | \n"); // Print column name & value
                            }

                        }
                        Console.WriteLine("QueryDB Lines affected:" + command.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write($"SQL Method: {ex.Message}");
            }

        }
    }
}
