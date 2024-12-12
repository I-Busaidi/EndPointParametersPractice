using System.ComponentModel.DataAnnotations;

namespace EndpointParametersSolution.DTOs
{
    public class ProductInputDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.05, 9999999)]
        public decimal productPrice { get; set; }

        public string productCategory { get; set; } = "general";
    }
}
