
using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;

namespace AbstractGame
{
    public class AbstractGame
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Abstract Game \n"); // \n newline

            //Main Menu stuff
            string[] MainMenuOptions = {
            "Start new game",
            "Load game"
            };
            ShowText(MainMenuOptions);

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateGame();
                    break;
                case 2:
                    LoadGame(Console.ReadLine());
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
            string filePath = "C:\\Users\\Neo\\source\\repos\\ThrowAway\\ThrowAway\\resources.txt"; //make relative
            string gameData = File.ReadAllText(filePath);

            Console.Clear();
            Console.WriteLine("Game name \n");
            string gameName = Console.ReadLine();
            Console.WriteLine("Player name: \n");
            string name = Console.ReadLine();
            Console.WriteLine("Difficulty 1/2/3 (easy to hard) \n");
            int difficulty = Convert.ToInt32(Console.ReadLine());

            Game newGame = new Game(gameName, name, difficulty);
        }

        static void LoadGame(string gameName)
        {
            //ka
        }
    }
}