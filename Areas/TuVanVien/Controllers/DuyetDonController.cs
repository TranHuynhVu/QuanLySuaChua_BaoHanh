using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySuaChua_BaoHanh.Models;

namespace QuanLySuaChua_BaoHanh.Areas.TuVanVien.Controllers
{
    [Area("TuVanVien")]
    public class DuyetDonController : Controller
    {
        private readonly BHSC_DbContext _context;

        public DuyetDonController(BHSC_DbContext context)
        {
            _context = context;
        }

        public IActionResult DanhSachDon()
        {
            var danhSach = _context.PhieuSuaChuas
                .Include(p => p.KhachHang)
                .Where(p => p.TrangThai.ToLower() == "chuaduyet")
                .ToList();

            return View(danhSach);
        }

        public async Task<IActionResult> ChiTietDon(int id)
        {
           

            var phieu = await _context.PhieuSuaChuas
                .Include(p => p.KhachHang)
                .Include(p => p.ChiTietSuaChuas)
                    .ThenInclude(ct => ct.LinhKien)
                .FirstOrDefaultAsync(p => p.PhuongId.ToString() == p.PhuongId); // nếu cần

            if (phieu == null)
                return NotFound();

            return View(phieu);
        }


        [HttpPost]
        public IActionResult Duyet(string id)
        {
            var phieu = _context.PhieuSuaChuas.Find(id);
            if (phieu != null)
            {
                phieu.TrangThai = "DaDuyet";
                _context.SaveChanges();
            }
            return RedirectToAction("DanhSachDon");
        }
    }
}
