namespace SWP391API.DTO
{
    public class UserRegisterDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
