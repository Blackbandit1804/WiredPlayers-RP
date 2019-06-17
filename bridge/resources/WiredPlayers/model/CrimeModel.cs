using System;

namespace WiredPlayers.model
{
    public class CrimeModel
    {
        public string crime { get; set; }
        public int jail { get; set; }
        public int fine { get; set; }
        public string reminder { get; set; }

        public CrimeModel(string crime, int jail, int fine, string reminder)
        {
            this.crime = crime;
            this.jail = jail;
            this.fine = fine;
            this.reminder = reminder;
        }
    }
}