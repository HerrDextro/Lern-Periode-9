using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGame.world
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; }
        public int Quality { get; set; }
        public int Availability { get; set; }
        public List<string> Tags { get; set; }
    }

    public class WpnGun : Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string AmmoType { get; set; }
        public int MagSize { get; set; }
        public int RPM { get; set; }
        public int Durability { get; set; }
        public int DurabilityMod { get; set; }
        public string Compatibility {  get; set; }
        public float Weight { get; set; }
        public int Quality { get; set; }
        public int Availability { get; set; }
    }
   
}
