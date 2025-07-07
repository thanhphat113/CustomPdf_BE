using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class ThuocTinhMau
{
    public int IdThuocTinh { get; set; }

    public int IdMau { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public int FontSize { get; set; }

    public int? FontSizeGiaTri { get; set; }

    public string Mau { get; set; } = null!;

    public string? MauGiaTri { get; set; }

    public bool InDam { get; set; }

    public bool? InDamGiaTri { get; set; }

    public bool Nghieng { get; set; }

    public bool? InNghiengGiaTri { get; set; }

    public bool GachChan { get; set; }

    public bool? GachChanGiaTri { get; set; }

    public bool UpperCase { get; set; }

    public bool? UpperCaseGiaTri { get; set; }

    public int? Rong { get; set; }

    public bool TrangThai { get; set; }

    public bool Stt { get; set; }

    public virtual DauCham? DauCham { get; set; }

    public virtual MauPdf IdMauNavigation { get; set; } = null!;

    public virtual ThuocTinh IdThuocTinhNavigation { get; set; } = null!;

    public virtual Ovuong? Ovuong { get; set; }
}
