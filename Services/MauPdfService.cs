using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomPdf_BE.Services
{
    public interface IMauPdfService
    {
        Task<dynamic> GetAll();
        Task<dynamic> GetById(int id);
    }
    public class MauPdfService : IMauPdfService
    {
        private readonly PdfFormatContext _context;

        public MauPdfService(PdfFormatContext context)
        {
            _context = context;
        }
        public async Task<dynamic> GetAll()
        {
            try
            {
                var result = await _context.MauPdfs.ToListAsync();
                return ServiceResult<List<MauPdf>>.SuccessResult(result, "Lấy danh sách thành công");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<List<MauPdf>>.Failure(new List<string> { ex.Message });
            }
        }

        public async Task<dynamic> GetById(int id)
        {
            try
            {
                var result = await _context.MauPdfs.FindAsync(id);
                if (result == null)
                    return ServiceResult<MauPdf>.SuccessResult(null, $"Không tìm thấy mẫu có id là {id}");

                return ServiceResult<MauPdf>.SuccessResult(result, $"Lấy mẫu pdf thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<MauPdf>.Failure(new List<string> { ex.Message });
            }
        }
    }
}