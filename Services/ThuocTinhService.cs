using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CustomPdf_BE.Services
{
    public interface IThuocTinhService
    {
        Task<dynamic> GetAllByPdfId(int PdfId);
        Task<dynamic> UpdateAllElements(List<ThuocTinhMau> items);
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
                                            .ThenInclude(tt => tt.IdLoaiNavigation)
                                            .Include(tt => tt.Ovuong)
                                            .Include(tt => tt.DauCham)
                                            .ToListAsync();

                return ServiceResult<List<ThuocTinhMau>>.SuccessResult(result, "Lấy danh sách thuộc tính dựa trên mẫu pdf thành công");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<List<ThuocTinhMau>>.Failure(new List<string> { ex.Message });
            }
        }

        public async Task<dynamic> UpdateAllElements(List<ThuocTinhMau> items)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.BulkUpdateAsync(items);

                    var listOvuong = items.Select(i => i.Ovuong).Where(x => x != null).ToList();
                    var listDauCham = items.Select(i => i.DauCham).Where(x => x != null).ToList();

                    if (listOvuong.Any())
                        await _context.BulkUpdateAsync(listOvuong);

                    if (listDauCham.Any())
                        await _context.BulkUpdateAsync(listDauCham);

                    await transaction.CommitAsync();
                    return ServiceResult<string>.SuccessResult("", "Cập nhật thành công");
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<string>.Failure(new List<string> { ex.Message });
                }
            }
        }
    }
}