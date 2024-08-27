using System.ComponentModel.DataAnnotations;

namespace AutomobileServiceCenter_MasterDetailsInAPI.DTO
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
