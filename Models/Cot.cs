using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class Cot
{
    public int IdCot { get; set; }

    public string? TenCot { get; set; }

    public int? IdThuocTinh { get; set; }

    public virtual ThuocTinh? IdThuocTinhNavigation { get; set; }
}
