
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
{
    private readonly FarmaciaContext _context;

    public MedicamentoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Medicamento>> GetAllAsync()
    {
        return await _context.Medicamentos
        .Include(p => p.DetallesFacturas)
        .Include(p => p.ComprasProveedores)
        .ToListAsync();
    }

    public override async Task<Medicamento> GetByIdAsync(int id)
    {
        return await _context.Medicamentos
        .Include(p => p.DetallesFacturas)
        .Include(p => p.ComprasProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Consultas Requeridas

    public async Task<IEnumerable<Medicamento>> GetCantidadMenorA50()
    {
        return await _context.Medicamentos
            .Where(p => p.Stock < 50)
            .ToListAsync();
    }











    public async Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync()
    {
        var medicamentosNoVendidos = await (
            from m in _context.Medicamentos
            where !_context.DetalleFacturas.Any(df => df.IdMedicamentoFk == m.Id)
            select new
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).ToListAsync();

        return medicamentosNoVendidos;
    }
    public async Task<IEnumerable<Object>> GetInfoMedicamentoVendidos()
    {
        var result = await
        (
            from f in _context.Facturas
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            where f.FechaCreacion >= new DateTime(2023, 3, 1) && f.FechaCreacion <= new DateTime(2023, 3, 31)
            select df.Cantidad
        ).SumAsync();

        return new List<Object> { new { Total = result } };

    }

    public async Task<IEnumerable<Object>> GetInfoMedicamentoMenosVendido()
    {
        var result = (
            from f in _context.Facturas
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            where f.FechaCreacion >= new DateTime(2023, 1, 1)
            group df by df.IdMedicamentoFk into grouped
            orderby grouped.Sum(df => df.Cantidad) ascending
            select new
            {
                IdMedicamentoFK = grouped.Key,
                Total = grouped.Sum(df => df.Cantidad)
            }
        ).FirstOrDefault();

        return await Task.FromResult(new List<Object> { result });
    }

    public async Task<IEnumerable<object>> ObtenerTotalMedicamentosVendidosPorMesEn2023Async()
    {
        var totalMedicamentosPorMes = await (
            from f in _context.Facturas
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            join m in _context.Medicamentos on df.IdMedicamentoFk equals m.Id
            where f.FechaCreacion.Year == 2023
            group new { f.FechaCreacion, m.Nombre, df.Cantidad } by new { f.FechaCreacion.Year, f.FechaCreacion.Month, m.Nombre } into g
            orderby g.Key.Year, g.Key.Month
            select new
            {
                mes = g.Key.Year + "-" + g.Key.Month.ToString("00"),
                medicamento = g.Key.Nombre,
                total = g.Sum(x => x.Cantidad)
            }
        ).ToListAsync();

        return totalMedicamentosPorMes;
    }

    

    public async Task<IEnumerable<Object>> GetInfoPromedioMedicamento()
    {
        var promedioMedicamentosCompradosPorVenta = await Task.Run(() =>
        {
            return (
                from cp in _context.CompraProveedores
                join df in _context.DetalleFacturas on cp.IdMedicamentoFk equals df.IdMedicamentoFk
                select cp.Cantidad
            ).Average();
        });

        return new List<Object> { new { Promedio = promedioMedicamentosCompradosPorVenta } };
    }

    public async  Task<IEnumerable<Object>> GetInfoMedicamentoVencidos()
    {
        DateTime fechaInicio = new DateTime(2024, 1, 1);
        DateTime fechaFin = new DateTime(2024, 12, 31);

        var medicamentos = await _context.Medicamentos
        .Where(m => m.FechaVencimiento >= fechaInicio && m.FechaVencimiento <= fechaFin)
        .ToListAsync();

        return medicamentos;
    }


}