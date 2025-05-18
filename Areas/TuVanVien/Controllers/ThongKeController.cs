using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLySuaChua_BaoHanh.Areas.TuVanVien.Controllers
{
    [Area("TuVanVien")]
    [Authorize(Roles = "TuVanVien")]
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
