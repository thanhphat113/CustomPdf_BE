using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class Ovuong
{
    public string? Rong { get; set; }

    public int IdThuocTinh { get; set; }

    public int IdMau { get; set; }

    public bool Visible { get; set; }

    public virtual ThuocTinhMau ThuocTinhMau { get; set; } = null!;
}
