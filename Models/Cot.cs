using System;
using System.Collections.Generic;

namespace CustomPdf_BE.Models;

public partial class Cot
{
    public int IdCot { get; set; }

    public string? TenCot { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public int? IdThuocTinh { get; set; }

    public string? TenCotDuLieu { get; set; }

    public int? IdCotCha { get; set; }

    public virtual Cot? IdCotChaNavigation { get; set; }

    public virtual ThuocTinh? IdThuocTinhNavigation { get; set; }

    public virtual ICollection<Cot> InverseIdCotChaNavigation { get; set; } = new List<Cot>();
}
