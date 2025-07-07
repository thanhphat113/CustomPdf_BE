using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class OVuongDTO
    {
        public int IdThuocTinh { get; set; }

        public int IdMau { get; set; }
        public bool Visible { get; set; }
        public List<int> List { get; set; } = new List<int>();
    }
}