using Graphic_Renderer;
using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;
using AbstractGame.debug;
using System.Data.SQLite;
using AbstractGame.systems;
using System.Xml.Linq;

namespace AbstractGame
{
    public class AbstractGame
    {
        static public FactionData factions;

        public AbstractGame() 
        {
            //Faction faction = new Faction();
            factions = JSONLoader.LoadFactions("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json");
            //factions = FactionLoader.LoadFactions("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json") ?? new FactionData();
            
        }
        static void Main()
        {
            Console.WriteLine("Welcome to the Abstract Game \n"); // \n newline

            //SReader reader = new SReader();

            //Main Menu stuff
            string[] MainMenuOptions = {
            "Start new game",
            "Load game",
            "Settings"
            };
            ShowText(MainMenuOptions);

            int choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    CreateNewGame();
                    break;
                case 2:
                    LoadGame();
                    break;
                case 3:
                    
                    break;
                case 4:
                    Debug.Query();
                    break;
            }    
        }
        private static void ShowText(string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i], 3, i + 1);
            }   
        }
        public static void CreateNewGame() //needs constraints/exception handling, also maybe try make this cleaner.
        {
            try //chatGPT
            {
                factions = JSONLoader.LoadFactions("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json") ?? new FactionData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading faction data: {ex.Message}");
                factions = new FactionData(); // Ensure it's not null
            }

            //Console.Clear(); //removed for bedug purposes
            Console.WriteLine("Game name");
            string gameName = Console.ReadLine();
            Console.WriteLine("Player name");
            string name = Console.ReadLine();


            //faction selection 
            Console.WriteLine("Choose a faction: (case sensitive)");
            foreach (var faction in factions.Factions.Keys) //Nullref on improper JSON
            {
                Console.WriteLine($"- {faction}");
            }
            string factionChoice = Console.ReadLine();
            string psChoice = "Random"; //"Random" per default to avoid "use of unassigned var"

            if (factions.Factions.TryGetValue(factionChoice, out Faction selectedFaction))
            {
                Console.WriteLine($"\n{factionChoice}: {selectedFaction.description}");
                Console.WriteLine("\nAvailable playstyles:");
                foreach (var playstyle in selectedFaction.Playstyles)
                {
                    Console.WriteLine($"- {playstyle.Key}: {playstyle.Value}");
                }
                psChoice = Console.ReadLine(); //SEE IF IT STAYS ON RANDOM, IF TRUE THEN FIX THIS SYSTEM
            }
            else
            {
                Console.WriteLine("Invalid faction choice.");
            }
            Console.WriteLine("Choose difficulty: \n -1=easy -2=normal-3=realistic");
            int diffChoice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            DBManager.SetDatabasePath(gameName);
            DBManager.CreateDB(gameName);
           
            DBManager.InitDB(); //initalize DB whenever in main creation menu, otherwise just load it.
            var connection = new SQLiteConnection(DBManager.connectionString);
            connection.Open();
            DBManager.    DBInsert(connection, "Player", new Dictionary<string, object>
            {
            { "Name", name },
            { "Faction", factionChoice },
            { "Playstyle", psChoice },
            { "Difficulty", diffChoice }
            });
            Debug.SelectAll("Player");
            //Game newGame = new Game(gameName, name, factionChoice, psChoice, diffChoice); 
        }
        static void LoadGame()
        {
            //merely showing the only table rn
            Console.WriteLine("enter Game to load");
            string loadGame = Console.ReadLine();
            DBManager.SetDatabasePath(loadGame);
            Debug.SelectAll("Player");
        }
    }
}


