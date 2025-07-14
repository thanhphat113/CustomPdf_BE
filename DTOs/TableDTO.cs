using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class TableDTO
    {
        public int IdThuocTinh { get; set; }
        public int IdMau { get; set; }
        public string Mau { get; set; } = null!;
        public bool InDam { get; set; }
        public bool Nghieng { get; set; }
        public bool GachChan { get; set; }
        public bool UpperCase { get; set; }
        public bool TrangThai { get; set; }

        public bool Stt { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public ICollection<CotDTO> Cots { get; set; }
    }
}