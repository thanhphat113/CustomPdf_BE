using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.DTOs;

namespace CustomPdf_BE.Helper
{
    public static class DataConverter
    {
        public static List<List<ThuocTinhDTO>> FormatToDrawPdf(List<ThuocTinhDTO> items)
        {
            var grouped = items
                .Where(i => i.TrangThai)
                .GroupBy(e => e.Y)
                .Select(group => group.OrderBy(e => e.X).ToList())
                .ToList();

            return grouped;
        }
    }
}