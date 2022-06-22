using System.ComponentModel.DataAnnotations.Schema;

namespace APITaskDot.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }

        public bool DisplayStatus { get; set; }
    }
}
