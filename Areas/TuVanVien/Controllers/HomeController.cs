using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySuaChua_BaoHanh.Models;

namespace QuanLySuaChua_BaoHanh.Areas.TuVanVien.Controllers
{
    [Area("TuVanVien")]
    [Authorize(Roles = "TuVanVien")]
    public class HomeController : Controller
    {
        private readonly BHSC_DbContext _context;

        public HomeController(BHSC_DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestOrders = await _context.PhieuSuaChuas
                .Include(p => p.KhachHang)
                .OrderByDescending(p => p.NgayGui)
                .Take(5)
                .ToListAsync();

            return View(latestOrders);
        }
    }
}
