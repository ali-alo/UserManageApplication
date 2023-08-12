using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManageApplication.Data;

namespace UserManageApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await GetUserAsync(userId);
            if (user == null)
                return;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetUserAsync(string userId) =>  await _userManager.FindByIdAsync(userId);

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task LockoutUser(string userId) => await ModifyUser(userId, true);

        public async Task UnlockUser(string userId) => await ModifyUser(userId, false);

        private async Task ModifyUser(string userId, bool deactivate)
        {
            var user = await GetUserAsync(userId);
            if (user == null)
                return;
            await _userManager.SetLockoutEnabledAsync(user, deactivate);
            await _userManager.SetLockoutEndDateAsync(user, deactivate ? new DateTime(3999, 01, 01) : DateTime.UtcNow - TimeSpan.FromMinutes(1));
            user.IsActive = !deactivate;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoginTime(ApplicationUser user)
        {
            user.LastLoginTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
