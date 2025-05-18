using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySuaChua_BaoHanh.Areas.QuanTriVien.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Tên vai trò không được để trống!")]
        [Display(Name = "Tên vai trò")]
        public string Name { get; set; } = null!;
    }

    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> SelectedRoles { get; set; }
    }
}
