namespace FlowersLab.Models
{
    public class Flower
    {
        public Flower()
        {
            Bouquets = new List<Bouquet>();
        }

        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public string FlowerDescription { get; set; }
        public virtual ICollection<Bouquet> Bouquets { get; set; }
    }
}
