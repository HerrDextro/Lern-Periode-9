using Graphic_Renderer;
using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;
using AbstractGame.entities;

namespace AbstractGame
{
    public class AbstractGame
    {
        static public FactionData factions;

        public AbstractGame() 
        {
            //Faction faction = new Faction();
            factions = FactionLoader.LoadFactions("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json");
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
                    CreateGame();
                    break;
                case 2:
                    LoadGame();
                    break;
                case 3:
                    
                    break;
                case 4: //for debug, just insert whatever path for JSON, will output everything in it.
                    FactionLoader.TestJSON("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json");
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

        static void CreateGame() //needs constraints/exception handling
        {
            try //chatGPT
            {
                factions = FactionLoader.LoadFactions("C:\\Users\\Neo\\source\\repos\\AbstractGame\\AbstractGame\\resources\\gameOptions.json") ?? new FactionData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading faction data: {ex.Message}");
                factions = new FactionData(); // Ensure it's not null
            }

            //Console.Clear(); //removed for bedug purposes
            Console.WriteLine("game name");
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
            string psChoice = "random"; //random per default to avoi

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
            Console.WriteLine("Choose difficulty: \n easy-normal-realistic");
            int diffChoice = Convert.ToInt32(Console.ReadLine());
            Game newGame = new Game(gameName, name, factionChoice, psChoice, diffChoice); 
        }

        static void LoadGame()
        {
            //ka
        }
    }
}


