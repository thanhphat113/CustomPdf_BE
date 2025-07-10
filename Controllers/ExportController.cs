using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomPdf_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomPdf_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IExportPdfService _export;
        public ExportController(IExportPdfService export)
        {
            _export = export;
        }
        [HttpGet("export")]
        public async Task<IActionResult> Get()
        {
            return File(await _export.ExportPhieuDKKhamBenh(), "application/pdf", "phieu.pdf");
        }
    }
}