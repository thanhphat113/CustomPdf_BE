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
        private readonly IMapper _mapper;

        public ThuocTinhController(IThuocTinhService tt, IMapper mapper)
        {
            _tt = tt;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _tt.GetAllByPdfId(id);
            var mapper = _mapper.Map<List<ThuocTinhDTO>>(result.data);
            return Ok(mapper);
        }
    }
}