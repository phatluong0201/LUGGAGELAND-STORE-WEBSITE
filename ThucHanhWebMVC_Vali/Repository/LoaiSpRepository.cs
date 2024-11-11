using ThucHanhWebMVC_Vali.Models;

namespace ThucHanhWebMVC_Vali.Repository
{
    public class LoaiSpRepository : I_LoaiSpRepository
    {
        private readonly QlbanVaLiContext _context;

        public LoaiSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }

        public TLoaiSp Add(TLoaiSp loaiSp)
        {
            _context.TLoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public TLoaiSp Delete(String loaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(String maloaiSp)
        {
            return _context.TLoaiSps.Find(maloaiSp); // Kiểm tra lại kiểu dữ liệu của maloaiSp nếu cần
        }

        public TLoaiSp Update(TLoaiSp loaiSp)
        {
            _context.Update(loaiSp); // Cập nhật đối tượng loaiSp
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
