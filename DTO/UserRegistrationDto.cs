using System.ComponentModel.DataAnnotations;

namespace AutomobileServiceCenter_MasterDetailsInAPI.DTO
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
