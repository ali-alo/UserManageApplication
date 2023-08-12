using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserManageApplication.DTOs;

namespace UserManageApplication.ViewModels
{
    public class UsersReadEditDeleteModel
    {
        [BindNever]
        public List<UserDto> Users { get; set; } = new ();
        public List<CheckBox> CheckBoxes { get; set; } = new ();
        public Operation Operation { get; set; }
    }
}
