using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThucHanhWebMVC_Vali.Models;
using ThucHanhWebMVC_Vali.Models.Authentication;
using ThucHanhWebMVC_Vali.ViewModels;
using X.PagedList;

namespace ThucHanhWebMVC_Vali.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication] // Kiểm tra người dùng đã đăng nhập chưa
        public IPagedList<TDanhMucSp> GetPagedList(int pageNumber, int pageSize)
        {
            return db.TDanhMucSps.AsNoTracking()
                                 .OrderBy(x => x.TenSp)
                                 .ToPagedList(pageNumber, pageSize);
        }

        public IActionResult Index(int? page)
        {
            // Kiểm tra session, nếu người dùng đã đăng nhập, ghi log
            var userName = HttpContext.Session.GetString("UserName");
            if (userName != null)
            {
                _logger.LogInformation($"User {userName} is logged in and viewing the home page.");
            }
            else
            {
                _logger.LogWarning("User is not logged in.");
            }

            int pageSize = 8;  // Số sản phẩm mỗi trang
            int pageNumber = (page ?? 1);  // Nếu không có page thì mặc định là 1

            // Lấy danh sách sản phẩm đã phân trang
            var list_san_pham = GetPagedList(pageNumber, pageSize);  // Gọi phương thức phân trang chung

            return View(list_san_pham);  // Trả về View với danh sách phân trang
        }

        public IActionResult SanPhamTheoLoai(string maLoai, int page = 1)
        {
            int pageSize = 8;  // Số sản phẩm mỗi trang

            // Lấy danh sách sản phẩm theo loại và phân trang
            var list_Sp = db.TDanhMucSps
                             .Where(x => x.MaLoai == maLoai)
                             .OrderBy(x => x.TenSp)
                             .ToPagedList(page, pageSize);  // Đảm bảo trả về PagedList

            ViewData["MaLoai"] = maLoai;  // Truyền mã loại cho view để giữ lại trong URL phân trang

            return View(list_Sp);  // Trả về model là PagedList
        }

        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSp = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();

            // Assign to ViewBag.anhSanPham, which matches the view's expectation
            ViewBag.anhSanPham = anhSp;

            return View(sanPham);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSp = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewMdel = new HomeProductDetailViewModel { danhMucSp = sanPham, anhSps = anhSp };

            return View(homeProductDetailViewMdel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
