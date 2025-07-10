using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.DTOs
{
    public class TableDTO
    {
        public double X { get; set; }

        public double Y { get; set; }

        public ICollection<CotDTO> Cots { get; set; }
    }
}