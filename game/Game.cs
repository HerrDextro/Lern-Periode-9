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
        //public FactionData factions; moved to program.cs
        public World World { get; private set; }
        public Actor Player { get; private set; }
        public LifeNet LifeNet { get; private set; }

        public Game(string gameNameInput, string playerName, string faction, string playstyle, int difficulty) //construct game, need params for world and NPC???? (prop yes)
        {
            gameName = gameNameInput;
            difficultyLevel = difficulty;
            isRunnung = true;
            //factions = FactionLoader.LoadFactions("resources/gameOptions.json"); moved to program.cs

            //World = new World(difficultyLevel);
            Player = new Actor();
            LifeNet = new LifeNet();
        }

    }
}
