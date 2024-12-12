using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndpointParametersSolution.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }

        [Required]
        [StringLength(50)]
        public string productName { get; set; }

        [StringLength(50)]
        public string productCategory { get; set; }

        [Required]
        public decimal productPrice { get; set; } 

        public DateTime dateAdded { get; set; } = DateTime.Now;
    }
}
