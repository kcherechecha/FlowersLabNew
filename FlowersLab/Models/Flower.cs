using System.ComponentModel.DataAnnotations;

namespace FlowersLab.Models
{
    public class Flower
    {
        public Flower()
        {
            Bouquets = new List<Bouquet>();
        }

        public int FlowerId { get; set; }
        [Required]
        public string FlowerName { get; set; }
        [Required]
        public string FlowerDescription { get; set; }
        public virtual ICollection<Bouquet> Bouquets { get; set; }
    }
}
