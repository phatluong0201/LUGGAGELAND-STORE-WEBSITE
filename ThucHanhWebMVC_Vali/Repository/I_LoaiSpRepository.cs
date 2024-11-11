using ThucHanhWebMVC_Vali.Models;
using ThucHanhWebMVC_Vali.Models;
namespace ThucHanhWebMVC_Vali.Repository
{
    public interface I_LoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp loaiSp);
        TLoaiSp Update(TLoaiSp loaiSp);
        TLoaiSp Delete(String loaiSp);
        TLoaiSp GetLoaiSp(String loaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();


    }
}
