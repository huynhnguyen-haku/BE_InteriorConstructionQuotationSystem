using System.ComponentModel.DataAnnotations;

namespace SWP391API.DTO
{
    public class VerifyTokenDTO
    {
        [Required]
        public string Token { get; set; }

        public VerifyTokenDTO(string token)
        {
            Token = token;
        }
    }
}
