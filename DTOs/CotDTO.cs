using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class CotDTO
    {
        public int IdCot { get; set; }

        public string? TenCot { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }

        public int? IdThuocTinh { get; set; }

        public string? TenCotDuLieu { get; set; }

        public int? IdCotCha { get; set; }
    }
}