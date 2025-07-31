using Helicoil_Smart.Application.DTOs;
using Helicoil_Smart.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Helicoil_Smart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly IRegistroService _service;

        public RegistrosController(IRegistroService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistroDto dto)
        {
            await _service.GuardarRegistroAsync(dto);
            return Ok(new { mensaje = "Registro guardado exitosamente." });
        }

        [HttpGet("exportar")]
        public async Task<IActionResult> Exportar(
            [FromQuery] string? nombre,
            [FromQuery] string? cedula,
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta)
        {
            var excel = await _service.ExportarExcelAsync(nombre, cedula, desde, hasta);
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "registros.xlsx");
        }
    }
}
