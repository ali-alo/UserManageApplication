namespace UserManageApplication.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsActive { get; set; }
    }
}
