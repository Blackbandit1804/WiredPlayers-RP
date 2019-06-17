using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class BusinessModel
    {
        public int id { get; set; }
        public int type { get; set; }
        public string ipl { get; set; }
        public string name { get; set; }
        public Vector3 position { get; set; }
        public uint dimension { get; set; }
        public string owner { get; set; }
        public int funds { get; set; }
        public int products { get; set; }
        public float multiplier { get; set; }
        public bool locked { get; set; }
        public TextLabel businessLabel { get; set; }
    }
}
