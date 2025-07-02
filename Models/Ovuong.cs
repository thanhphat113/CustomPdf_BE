using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class Ovuong
{
    public int? Rong { get; set; }

    public int IdThuocTinh { get; set; }

    public virtual ThuocTinh IdThuocTinhNavigation { get; set; } = null!;
}
