using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Helper;
using CustomPdf_BE.Models;
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
            var tables = await _tt.GetTablesByPdfId(id);

            if (!tables.Success) return BadRequest(ApiResponse<string>.ErrorResponse(elements.Message, elements.Errors));
            if (!pdf.Success) return BadRequest(ApiResponse<string>.ErrorResponse(pdf.Message, pdf.Errors));
            if (!elements.Success) return BadRequest(ApiResponse<string>.ErrorResponse(elements.Message, elements.Errors));

            var mapper = new
            {
                pdf = _mapper.Map<MauPdfDTO>(pdf.Data),
                elements = _mapper.Map<List<ThuocTinhDTO>>(elements.Data),
                tables = _mapper.Map<List<TableDTO>>(tables.Data)
            };

            return Ok(ApiResponse<dynamic>.SuccessResponse(mapper, "Lấy dữ liệu thành công"));
        }

        [HttpPut("Save")]
        public async Task<IActionResult> PutAsync([FromBody] RequestSaveItems items)
        {
            var mapperElements = _mapper.Map<List<ThuocTinhMau>>(items.elements);
            var mapperTables = _mapper.Map<List<ThuocTinhMau>>(items.tables);
            var combined = mapperElements.Concat(mapperTables).ToList();
            // return Ok(combined);
            var result = await _tt.UpdateAllElements(combined);
            // return Ok(result);

            if (!result.Success) return BadRequest(ApiResponse<string>.ErrorResponse(result.Message, result.Errors));
            return Ok(ApiResponse<string>.SuccessResponse(result.Data, result.Message));
        }
    }

    public class RequestSaveItems
    {
        public List<ThuocTinhDTO> elements { get; set; }
        public List<TableDTO> tables { get; set; }
    }
}