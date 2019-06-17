using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class JobPickModel
    {
        public int job { get; set; }
        public Vector3 position { get; set; }
        public string description { get; set; }

        public JobPickModel(int job, Vector3 position, string description)
        {
            this.job = job;
            this.position = position;
            this.description = description;
        }
    }
}
