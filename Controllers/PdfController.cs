using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomPdf_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IMauPdfService _pdf;
        private readonly IMapper _mapper;
        public PdfController(IMauPdfService pdf, IMapper mapper)
        {
            _pdf = pdf;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            var result = await _pdf.GetAll();

            if (!result.Success) return BadRequest(ApiResponse<string>.ErrorResponse(result.Message, result.Errors));
            var mapper = _mapper.Map<List<MauPdfDTO>>(result.Data);
            return Ok(ApiResponse<List<MauPdfDTO>>.SuccessResponse(mapper, result.Message));
        }
    }
}