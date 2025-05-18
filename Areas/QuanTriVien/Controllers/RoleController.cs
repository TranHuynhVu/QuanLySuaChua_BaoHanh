using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySuaChua_BaoHanh.Areas.QuanTriVien.Models;
using QuanLySuaChua_BaoHanh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLySuaChua_BaoHanh.Areas.QuanTriVien.Models;
using QuanLySuaChua_BaoHanh.Services;

namespace QuanLySuaChua_BaoHanh.Areas.QuanTriVien.Controllers
{
    [Area("QuanTriVien")]
    [Authorize(Roles = "QuanTriVien")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly UserManager<NguoiDung> _userManager;
        private readonly IDGenerator _idGenerator;

        public RoleController(RoleManager<IdentityRole<string>> roleManager, UserManager<NguoiDung> userManager, IDGenerator idGenerator)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _idGenerator = idGenerator;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            ModelState.Remove("Id");
                // Kiểm tra role đã tồn tại
               
            if (ModelState.IsValid)
            {
                 if (await _roleManager.RoleExistsAsync(model.Name))
                 {
                    ModelState.AddModelError("", "Role đã tồn tại!");
                    return View(model);
                 }

                 string roleId = await _idGenerator.GenerateRoleIdAsync(model.Name);
                model.Id = roleId;

                // Tạo role mới
                var identityRole = new IdentityRole<string>
                {
                    Id = model.Id,
                    Name = model.Name,
                    //NormalizedName = model.Name.ToUpper(),

                };

                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Tạo role thành công!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        //get: quantrivien/role/edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return NotFound();
                }

                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật role thành công!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        //get: quantrivien/role/delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Count > 0)
            {
                TempData["ErrorMessage"] = $"Không thể xóa role '{role.Name}' vì đang có {usersInRole.Count} người dùng.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Xóa role thành công!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Xóa role không thành công!";
            return RedirectToAction(nameof(Index));
        }

        //get: quantrivien/role/assignRoles
        public async Task<IActionResult> AssignRoles(string? userId)
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            var model = new UserRolesViewModel();

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    model.UserId = user.Id.ToString();
                    model.UserName = user.UserName;
                    model.Roles = await _userManager.GetRolesAsync(user);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng!";
                return RedirectToAction(nameof(AssignRoles));
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }

            if (model.SelectedRoles != null && model.SelectedRoles.Count > 0)
            {
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);
            }

            TempData["SuccessMessage"] = "Cập nhật quyền thành công!";
            return RedirectToAction(nameof(AssignRoles), new { userId = model.UserId });
        }
    }
}
