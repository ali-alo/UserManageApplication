using Microsoft.AspNetCore.Identity;

namespace UserManageApplication.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public DateTime RegistrationTime { get; set; }

        [PersonalData]
        public DateTime LastLoginTime { get; set; }

        [PersonalData]
        public bool IsActive { get; set; }
        [PersonalData]
        public string FullName { get; set; }
    }
}
