using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helicoil_Smart.Application.Interfaces
{
    using Application.DTOs;
    using Domain.Entities;

    public interface IRegistroService
    {
        Task GuardarRegistroAsync(RegistroDto dto);
        Task<byte[]> ExportarExcelAsync(string? nombre, string? cedula, DateTime? desde, DateTime? hasta);
    }

}
