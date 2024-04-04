using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Fullname { get; set; } = null!;


        public DateTime? Birthdate { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;


        public string? PhoneNumber { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
