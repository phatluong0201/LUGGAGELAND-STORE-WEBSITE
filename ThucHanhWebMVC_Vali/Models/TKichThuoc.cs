﻿using System;
using System.Collections.Generic;

namespace ThucHanhWebMVC_Vali.Models;

public partial class TKichThuoc
{
    public string MaKichThuoc { get; set; } = null!;

    public string? KichThuoc { get; set; }

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; } = new List<TChiTietSanPham>();
}
