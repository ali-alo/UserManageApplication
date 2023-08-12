using UserManageApplication.Data;

namespace UserManageApplication.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser?> GetUserAsync(string userId);
        Task UpdateLoginTime(ApplicationUser user);
        Task LockoutUser(string userId);
        Task UnlockUser(string userId);
        Task DeleteUser(string userId);
    }
}
