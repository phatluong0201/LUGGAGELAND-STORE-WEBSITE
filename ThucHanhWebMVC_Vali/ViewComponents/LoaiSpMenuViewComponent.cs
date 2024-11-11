using ThucHanhWebMVC_Vali.Models;
using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC_Vali.Repository;
namespace ThucHanhWebMVC_Vali.ViewComponents
{
    public class LoaiSpMenuViewComponent: ViewComponent
    {
        private readonly I_LoaiSpRepository _loaiSp;
        public LoaiSpMenuViewComponent(I_LoaiSpRepository i_LoaiSpRepository)
        {
            _loaiSp = i_LoaiSpRepository;
        }   
        public IViewComponentResult Invoke()
        {
            var loaiSP=_loaiSp.GetAllLoaiSp().OrderBy(x=>x.Loai);

            return View(loaiSP);
        }
    }
}
