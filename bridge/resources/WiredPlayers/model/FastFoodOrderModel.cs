using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class FastFoodOrderModel
    {
        public int id { get; set; }
        public int pizzas { get; set; }
        public int hamburgers { get; set; }
        public int sandwitches { get; set; }
        public Vector3 position { get; set; }
        public float distance { get; set; }
        public double limit { get; set; }
        public bool taken { get; set; }
    }
}
