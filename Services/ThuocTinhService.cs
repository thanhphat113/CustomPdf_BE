using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.DTOs;
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
                                            .ThenInclude(tt => tt.DauCham)
                                            .Include(tt => tt.IdThuocTinhNavigation)
                                            .ThenInclude(tt => tt.Ovuong)
                                            .Include(tt => tt.IdThuocTinhNavigation)
                                            .ThenInclude(item => item.IdLoaiNavigation)
                                            .ToListAsync();

                return ServiceResult<List<ThuocTinhMau>>.SuccessResult(result, "Lấy danh sách thuộc tính dựa trên mẫu pdf thành công");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<List<ThuocTinhMau>>.Failure(new List<string> { ex.Message });
            }
        }
    }
}