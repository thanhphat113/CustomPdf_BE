using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class ThuocTinh
{
    public int IdThuocTinh { get; set; }

    public string NoiDung { get; set; } = null!;

    public int? IdLoai { get; set; }

    public string? TenCotDuLieu { get; set; }

    public virtual ICollection<Cot> Cots { get; set; } = new List<Cot>();

    public virtual LoaiThuocTinh? IdLoaiNavigation { get; set; }

    public virtual ThuocTinhMau? ThuocTinhMau { get; set; }
}
