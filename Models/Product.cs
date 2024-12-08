using System.ComponentModel.DataAnnotations;

namespace PaggingSample.Models
{
# nullable disable
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
