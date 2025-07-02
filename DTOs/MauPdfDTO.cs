using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class MauPdfDTO
    {
        public int IdMau { get; set; }

        public string? TenMau { get; set; }

        public int? Rong { get; set; }

        public int? Dai { get; set; }
    }
}