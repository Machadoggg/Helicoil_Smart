using Helicoil_Smart.Application.DTOs;
using Helicoil_Smart.Application.Interfaces;
using Helicoil_Smart.Domain.Entities;
using Helicoil_Smart.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Helicoil_Smart.Application.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly AppDbContext _context;

        public RegistroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task GuardarRegistroAsync(RegistroDto dto)
        {
            var entity = new Registro
            {
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                FechaHora = dto.FechaHora,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud
            };

            _context.Registros.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> ExportarExcelAsync(string? nombre, string? cedula, DateTime? desde, DateTime? hasta)
        {
            var query = _context.Registros.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(r => r.Nombre.Contains(nombre));
            if (!string.IsNullOrEmpty(cedula))
                query = query.Where(r => r.Cedula.Contains(cedula));
            if (desde.HasValue)
                query = query.Where(r => r.FechaHora >= desde.Value);
            if (hasta.HasValue)
                query = query.Where(r => r.FechaHora <= hasta.Value);

            var lista = await query.ToListAsync();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Registros");

            ws.Cells[1, 1].Value = "Nombre";
            ws.Cells[1, 2].Value = "Cédula";
            ws.Cells[1, 3].Value = "FechaHora";
            ws.Cells[1, 4].Value = "Latitud";
            ws.Cells[1, 5].Value = "Longitud";

            int fila = 2;
            foreach (var r in lista)
            {
                ws.Cells[fila, 1].Value = r.Nombre;
                ws.Cells[fila, 2].Value = r.Cedula;
                ws.Cells[fila, 3].Value = r.FechaHora.ToString("yyyy-MM-dd HH:mm:ss");
                ws.Cells[fila, 4].Value = r.Latitud;
                ws.Cells[fila, 5].Value = r.Longitud;
                fila++;
            }

            return package.GetAsByteArray();
        }
    }
}
