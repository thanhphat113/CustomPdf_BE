using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class ThuocTinhDTO
    {
        public int IdThuocTinh { get; set; }

        public string? NoiDung { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }

        public int? FontSize { get; set; }
        public bool? Stt { get; set; }

        public bool? TrangThai { get; set; }

        public string TenLoai { get; set; }

        public Box Box { get; set; }

        public Dot Dot { get; set; }
    }

    public class Box
    {
        public bool Visible { get; set; }
        public List<int> DanhSach { get; set; }
    }

    public class Dot
    {
        public bool Visible { get; set; }
        public int Width { get; set; }
    }
}