using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UserManageApplication.Data;
using UserManageApplication.DTOs;
using UserManageApplication.Models;
using UserManageApplication.Repositories;
using UserManageApplication.ViewModels;

namespace UserManageApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!await ValidateUserActive())
                return RedirectToAction("Index");
            var users = await _userRepository.GetUsersAsync();
            var model = UsersToModel(users);
            return View(model);
        }

        private async Task<bool> ValidateUserActive()
        {
            var user = await _userRepository.GetUserAsync(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (user == null || (user != null && !user.IsActive))
            {
                await _signInManager.SignOutAsync();
                return false;
            }
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(UsersReadEditDeleteModel model)
        {
            if (!await ValidateUserActive() || !ModelState.IsValid)
                return RedirectToAction("Index");
            await EditUsers(model.CheckBoxes.Where(c => c.IsChecked), model.Operation); 
            return RedirectToAction("Index");
        }

        private async Task EditUsers(IEnumerable<CheckBox> checkboxes, Operation operation)
        {
            Func<string, Task> action;
            if (operation == Operation.Block)
                action = async (id) => await _userRepository.LockoutUser(id);
            else if (operation == Operation.Unblock)
                action = async (id) => await _userRepository.UnlockUser(id);
            else
                action = async (id) => await _userRepository.DeleteUser(id);
            foreach (var item in checkboxes)
                await action(item.Id);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private UsersReadEditDeleteModel UsersToModel(IEnumerable<ApplicationUser> users)
        {
            var usersDto = new List<UserDto>();
            var checkBoxes = new List<CheckBox>();
            foreach (var user in users)
            {
                usersDto.Add(UserToUserDto(user));
                checkBoxes.Add(new CheckBox { Id = user.Id });
            }
            var model = new UsersReadEditDeleteModel { Users = usersDto, Operation = (Operation)int.MaxValue, CheckBoxes = checkBoxes };
            return model;
        }

        private UserDto UserToUserDto(ApplicationUser user)
        {
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                LastLoginTime = user.LastLoginTime,
                RegistrationTime = user.RegistrationTime,
            };
            return userDto;
        }
        
    }
}