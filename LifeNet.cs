using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame
{
    public class LifeNet
    {
        //spawnrates factions, 1 to 10 per in game day
        public int factionlessRate;
        public int banditRate;
        public int citizenRate;
        public int grieverRate;
        public int eliteRate;
        public LifeNet(int difficulty) 
        {
            switch(difficulty)
            {
                case 1:
                    factionlessRate = 5;
                    banditRate = 3;
                    citizenRate = 4;
                    grieverRate = 2;
                    eliteRate = 0;
                    break;
                case 2:
                    factionlessRate = 5;
                    banditRate = 4;
                    citizenRate = 5;
                    grieverRate = 4;
                    eliteRate = 1;
                    break;
                case 3:
                    factionlessRate = 4;
                    banditRate = 6;
                    citizenRate = 2;
                    grieverRate = 6;
                    eliteRate = 2;
                    break;
            }
        }    
    }
}


