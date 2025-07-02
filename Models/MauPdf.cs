using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class MauPdf
{
    public int IdMau { get; set; }

    public string? TenMau { get; set; }

    /// <summary>
    /// Đơn vị (mm)
    /// </summary>
    public int? Rong { get; set; }

    /// <summary>
    /// Đơn vị (mm)
    /// </summary>
    public int? Dai { get; set; }

    public virtual ICollection<ThuocTinhMau> ThuocTinhMaus { get; set; } = new List<ThuocTinhMau>();
}
