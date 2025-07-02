using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class ThuocTinhMau
{
    public int IdThuocTinh { get; set; }

    public int IdMau { get; set; }

    public virtual MauPdf IdMauNavigation { get; set; } = null!;

    public virtual ThuocTinh IdThuocTinhNavigation { get; set; } = null!;
}
