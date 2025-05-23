using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame
{
    public class Actor
    {
        public string Name { get; set; }
        public string Description { get; set; } //optinal for now
        public string Faction { get; set; }
        public string PlayStyle { get; set; }
        public int Difficulty { get; set; }
        public int Health { get; set; }

    }
}
