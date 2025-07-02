using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class LoaiThuocTinh
{
    public int IdLoai { get; set; }

    public string? TenLoai { get; set; }

    public virtual ICollection<ThuocTinh> ThuocTinhs { get; set; } = new List<ThuocTinh>();
}
