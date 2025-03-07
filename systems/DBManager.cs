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
        static string dbPath = "gameSave.db";
        public static string connectionString = $"Data Source={dbPath};Version=3;";

        /*public static SQLiteConnection OpenConnection()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }*/
        public static void InitDB()
        {
            using (var connection = new SQLiteConnection(connectionString)) //only declare connection here not in later methods. (remove "var")
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
                    Difficulty INT NOT NULL
                );";

                using (var command = new SQLiteCommand(createTablesQuery, connection))
                {
                    Console.WriteLine("InitDB Lines affected:" + command.ExecuteNonQuery());
                }
            }

        }
        public static void DBInsert(SQLiteConnection connection, string tableName, Dictionary<string, object> values) //little param confusion
        {
            string columns = string.Join(", ", values.Keys);
            string parameters = string.Join(", ", values.Keys.Select(k => $"@{k}"));

            string selectQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using (var command = new SQLiteCommand(selectQuery, connection))
            {
                foreach (var kvp in values)
                {
                    command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
                }
                Console.WriteLine("DBInsert Lines affected:" + command.ExecuteNonQuery());
            }


        }
    }
}
