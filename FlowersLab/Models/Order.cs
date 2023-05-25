namespace FlowersLab.Models
{
    public class Order
    {
        public Order()
        {
            Bouquets = new List<Bouquet>();
        }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderAddress { get; set; }
        public virtual ICollection<Bouquet> Bouquets { get; set; }
    }
}
