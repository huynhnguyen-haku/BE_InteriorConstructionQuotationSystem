namespace SWP391API.DTO
{
    public class AddUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvtUrl { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }

    }

    public class UpdateUserDTO
    {
        public string Fullname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvtUrl { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }

    }

    public class DeleteUserDTO
    {
        public int UserId { get; set; }
    }

}
