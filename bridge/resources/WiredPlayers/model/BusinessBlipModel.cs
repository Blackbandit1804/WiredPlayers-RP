namespace WiredPlayers.model
{
    public class BusinessBlipModel
    {
        public int id { get; set; }
        public int blip { get; set; }

        public BusinessBlipModel(int id, int blip)
        {
            this.id = id;
            this.blip = blip;
        }
    }
}
