using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame
{
    public class Game
    {
        public string gameName;
        public int difficultyLevel;
        public bool isRunnung = false;
        public World World { get; private set; }
        public Player Player { get; private set; }
        public LifeNet LifeNet { get; private set; }

        public Game(string gameName, string playerName, int difficulty) //construct game, need params for world and NPC???? (prop yes)
        {
            isRunnung = true;

            World = new World(difficulty);
            Player = new Player(playerName, difficulty);
            LifeNet = new LifeNet(difficulty);


            string filePath = "C:\\Users\\Neo\\source\\repos\\ThrowAway\\ThrowAway\\gameData.txt"; //UNTESTED, and not yet any file creation
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"{gameName},{playerName},{difficulty}");
            };

        }

    }
}
