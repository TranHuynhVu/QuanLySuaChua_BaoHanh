using Microsoft.AspNetCore.Mvc;
using QuanLySuaChua_BaoHanh.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLySuaChua_BaoHanh.Areas.TuVanVien.Controllers
{
    [Area("TuVanVien")]
    public class CapNhatKHController : Controller
    {
        private readonly BHSC_DbContext _context;

        public CapNhatKHController(BHSC_DbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách KH
        public IActionResult CapNhatKhachHang(string? searchId)
        {
            var khachHangs = _context.NguoiDungs
                .Where(nd => nd.VaiTro == "KhachHang");

            if (!string.IsNullOrEmpty(searchId))
            {
                khachHangs = khachHangs.Where(nd => nd.Id == searchId);
            }

            return View(khachHangs.ToList());
        }

        // GET: Sửa thông tin
        public IActionResult Edit(string id)
        {
            var kh = _context.NguoiDungs.FirstOrDefault(nd => nd.Id == id && nd.VaiTro == "KhachHang");
            if (kh == null)
                return NotFound();

            return View("Sua", kh);
        }

        // POST: Cập nhật thông tin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NguoiDung model)
        {
            if (!ModelState.IsValid)
                return View("Sua", model);

            var kh = _context.NguoiDungs.FirstOrDefault(nd => nd.Id == model.Id && nd.VaiTro == "KhachHang");
            if (kh == null)
                return NotFound();

            kh.HoTen = model.HoTen;
            kh.Email = model.Email;
            kh.PhoneNumber = model.PhoneNumber;
            kh.DiaChi = model.DiaChi;
            kh.PhuongId = model.PhuongId;

            _context.SaveChanges();
            return RedirectToAction("CapNhatKhachHang");
        }

        // POST: Xóa 1 KH
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Xoa(string id)
        {
            var kh = _context.NguoiDungs.Find(id);
            if (kh != null)
            {
                _context.NguoiDungs.Remove(kh);
                _context.SaveChanges();
            }
            return RedirectToAction("CapNhatKhachHang");
        }

        // POST: Xoá nhiều
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaNhieu(List<string> selectedIds)
        {
            if (selectedIds != null && selectedIds.Count > 0)
            {
                var khachHangs = _context.NguoiDungs
                    .Where(nd => selectedIds.Contains(nd.Id) && nd.VaiTro == "KhachHang")
                    .ToList();

                _context.NguoiDungs.RemoveRange(khachHangs);
                _context.SaveChanges();
            }
            return RedirectToAction("CapNhatKhachHang");
        }
    }
}
