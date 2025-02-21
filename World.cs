using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame
{
    public class World
    {
        public int difficulty;
        public string weather;

        public World(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    weather = "warm, calm";
                    break;
                case 2:
                    weather = "dynamic, temperate";
                    break;
                case 3:
                    weather = "bad, cold";
                    break;
            }
        }

        
    }
}
