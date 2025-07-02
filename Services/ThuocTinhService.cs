using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomPdf_BE.Services
{
    public interface IThuocTinhService
    {
        Task<dynamic> GetAllByPdfId(int PdfId);
    }
    public class ThuocTinhService : IThuocTinhService
    {
        private readonly PdfFormatContext _context;

        public ThuocTinhService(PdfFormatContext context)
        {
            _context = context;
        }
        public async Task<dynamic> GetAllByPdfId(int PdfId)
        {
            try
            {
                var result = await _context.ThuocTinhMaus
                                            .Where(tt => tt.IdMau == PdfId)
                                            .Include(tt => tt.IdThuocTinhNavigation)
                                            .ThenInclude(item => item.IdLoaiNavigation)
                                            .ToListAsync();

                return new
                {
                    statusCode = 400,
                    data = result
                };
            }
            catch (System.Exception ex)
            {
                return new
                {
                    statusCode = 500,
                    message = ex.Message
                };
            }
        }
    }
}