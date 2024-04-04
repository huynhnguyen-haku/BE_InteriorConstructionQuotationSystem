using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class ResendTokenDTO
    {
        [Required]
        public string Email { get; set; }

        public ResendTokenDTO(string email)
        {
            Email = email;
        }
    }
}
