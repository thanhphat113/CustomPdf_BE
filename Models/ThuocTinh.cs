using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class ThuocTinh
{
    public int IdThuocTinh { get; set; }

    public string NoiDung { get; set; } = null!;

    public double X { get; set; }

    public double Y { get; set; }

    public int FontSize { get; set; }

    public double? Rong { get; set; }

    public bool TrangThai { get; set; }

    public int IdLoai { get; set; }

    public bool Stt { get; set; }

    public virtual ICollection<Cot> Cots { get; set; } = new List<Cot>();

    public virtual DauCham? DauCham { get; set; }

    public virtual LoaiThuocTinh IdLoaiNavigation { get; set; } = null!;

    public virtual Ovuong? Ovuong { get; set; }

    public virtual ThuocTinhMau? ThuocTinhMau { get; set; }
}
