namespace FlowersLab.Models
{
    public class Bouquet
    {
        public Bouquet()
        {
            Flowers = new List<Flower>();
            Orders = new List<Order>();
        }
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
        public virtual ICollection<Flower> Flowers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
