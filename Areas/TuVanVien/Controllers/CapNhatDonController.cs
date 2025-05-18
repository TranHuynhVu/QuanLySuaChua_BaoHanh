using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using QuanLySuaChua_BaoHanh.Models;

namespace QuanLySuaChua_BaoHanh.Areas.TuVanVien.Controllers
{
    [Area("TuVanVien")]
    [Authorize(Roles = "TuVanVien")]
    public class CapNhatDonController : Controller
    {
        private readonly BHSC_DbContext _context;

        public CapNhatDonController(BHSC_DbContext context)
        {
            _context = context;
        }

        public IActionResult CapNhatDon()
        {
            var donHangs = _context.PhieuSuaChuas
                            .Include(p => p.KhachHang)
                            .ToList();

            return View("CapNhatDon", donHangs);
        }

        public IActionResult CapNhatTrangThaiDon(string id)
        {
            var phieu = _context.PhieuSuaChuas
                         .Include(p => p.KhachHang)
                         .FirstOrDefault(p => p.PhieuSuaChuaId == id);

            if (phieu == null)
                return NotFound();

            return View("CapNhatTrangThaiDon", phieu);
        }
        public IActionResult CapNhatTrangThai(string id, string trangThaiMoi)
        {
            var phieu = _context.PhieuSuaChuas.FirstOrDefault(p => p.PhieuSuaChuaId == id);
            if (phieu == null) return NotFound();

            phieu.TrangThai = trangThaiMoi;
            _context.SaveChanges();

            return RedirectToAction("CapNhatDon");
        }
        [HttpGet]
        public IActionResult ThemDon()
        {
            return View("ĐKOFF");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDon(PhieuSuaChua model)
        {
            if (ModelState.IsValid)
            {
                _context.PhieuSuaChuas.Add(model);
                _context.SaveChanges();
                return RedirectToAction("CapNhatDon");
            }

            // Nếu không hợp lệ, trả về form để hiển thị lỗi
            return View("ĐKOFF", model);
        }
    }
}
