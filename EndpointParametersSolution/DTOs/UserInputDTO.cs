using System.ComponentModel.DataAnnotations;

namespace EndpointParametersSolution.DTOs
{
    public class UserInputDTO
    {
        [Required(ErrorMessage = "User name is required")]
        [StringLength(35)]
        public string userName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email must be in this pattern: example@example.com")]
        public string userEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be 8 to 20 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 1 letter, 1 number")]
        public string userPassword { get; set; }
    }
}
