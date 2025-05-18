using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySuaChua_BaoHanh.Models;
using QuanLySuaChua_BaoHanh.Services;
using QuanLySuaChua_BaoHanh.ViewModels;

namespace QuanLySuaChua_BaoHanh.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<NguoiDung> _userManager;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly BHSC_DbContext _context;
        private readonly IDGenerator _idGenerator;


        public AccountController(
            UserManager<NguoiDung> userManager,
            SignInManager<NguoiDung> signInManager,
            BHSC_DbContext context,
            IDGenerator idGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _idGenerator = idGenerator;
        }



        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Phuongs = new SelectList(await _context.Phuongs.ToListAsync(), "PhuongId", "TenPhuong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new NguoiDung
                {
                    Id = await _idGenerator.GenerateNguoiDungIDAsync("KhachHang"),
                    UserName = model.UserName,
                    Email = model.Email,
                    HoTen = model.HoTen,
                    PhoneNumber = model.PhoneNumber,
                    DiaChi = model.DiaChi,
                    PhuongId = model.PhuongId.ToString(),
                    VaiTro = "KhachHang"
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "KhachHang");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { area = "KhachHang"});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Phuongs = new SelectList(await _context.Phuongs.ToListAsync(), "PhuongId", "TenPhuong");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                // string[] roleNames = { "QuanTriVien", "KhachHang", "KyThuatVien", "NhanVienKho", "TuVanVien" };
                if (await _userManager.IsInRoleAsync(user, "KhachHang"))
                {
                    return RedirectToAction("Index", "Home", new { area = "KhachHang" });
                }
                else if (await _userManager.IsInRoleAsync(user, "QuanTriVien"))
                {
                    return RedirectToAction("Index", "Home", new { area = "QuanTriVien" });
                }
                else if (await _userManager.IsInRoleAsync(user, "KyThuatVien"))
                {
                    return RedirectToAction("Index", "Home", new { area = "KyThuatVien" });
                }
                else if (await _userManager.IsInRoleAsync(user, "NhanVienKho"))
                {
                    return RedirectToAction("Index", "Home", new { area = "NhanVienKho" });
                }
                else if (await _userManager.IsInRoleAsync(user, "TuVanVien"))
                {
                    return RedirectToAction("Index", "Home", new { area = "TuVanVien" });
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    //Điều hướng đến trang tương ứng với vai trò của người dùng
                    if (await _userManager.IsInRoleAsync(user, "QuanTriVien"))
                        return RedirectToAction("Index", "Home", new { area = "QuanTriVien" });
                    if (await _userManager.IsInRoleAsync(user, "KhachHang"))
                        return RedirectToAction("Index", "Home", new { area = "KhachHang" });
                    if (await _userManager.IsInRoleAsync(user, "KyThuatVien"))
                        return RedirectToAction("Index", "Home", new { area = "KyThuatVien" });
                    if (await _userManager.IsInRoleAsync(user, "NhanVienKho"))
                        return RedirectToAction("Index", "Home", new { area = "NhanVienKho" });
                    if (await _userManager.IsInRoleAsync(user, "TuVanVien"))
                        return RedirectToAction("Index", "Home", new { area = "TuVanVien" });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập không thành công.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
