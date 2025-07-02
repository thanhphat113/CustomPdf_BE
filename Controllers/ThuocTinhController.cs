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
    public class ThuocTinhController : ControllerBase
    {
        private readonly IThuocTinhService _tt;
        private readonly IMauPdfService _pdf;
        private readonly IMapper _mapper;

        public ThuocTinhController(IMauPdfService pdf, IThuocTinhService tt, IMapper mapper)
        {
            _tt = tt;
            _pdf = pdf;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var elements = await _tt.GetAllByPdfId(id);
            var pdf = await _pdf.GetById(id);

            if (!pdf.Success) return BadRequest(ApiResponse<string>.ErrorResponse(pdf.Message, pdf.Errors));
            if (!elements.Success) return BadRequest(ApiResponse<string>.ErrorResponse(elements.Message, elements.Errors));

            var mapper = new
            {
                pdf = _mapper.Map<MauPdfDTO>(pdf.Data),
                elements = _mapper.Map<List<ThuocTinhDTO>>(elements.Data)
            };

            return Ok(ApiResponse<dynamic>.SuccessResponse(mapper, "Lấy dữ liệu thành công"));
        }
    }
}