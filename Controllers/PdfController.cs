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
    public class PdfController : ControllerBase
    {
        private readonly IMauPdfService _pdf;
        public PdfController(IMauPdfService pdf)
        {
            _pdf = pdf;
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            return Ok(await _pdf.GetAll());
        }
    }
}