using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomPdf_BE.Services
{
    public interface IMauPdfService
    {
        Task<dynamic> GetAll();
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
            return await _context.MauPdfs.ToListAsync();
        }
    }
}