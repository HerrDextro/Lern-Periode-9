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

        public Game(string gameNameInput, string playerName, int difficulty) //construct game, need params for world and NPC???? (prop yes)
        {
            gameName = gameNameInput;
            
            isRunnung = true;

            World = new World(difficulty);
            Player = new Player(playerName, difficulty);
            LifeNet = new LifeNet(difficulty);
        }

    }
}
