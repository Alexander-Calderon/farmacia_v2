
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IEnumerable<object>> TotalMedicamentosVendidosPorProveedorAsync()
    {
        var totalMedicamentosPorProveedor = await (
            from p in _context.Proveedores
            join cp in _context.CompraProveedores on p.Id equals cp.IdProveedorFk
            group cp by new { p.Id, p.Nombre } into g
            select new
            {
                IdProveedor = g.Key.Id,
                NombreProveedor = g.Key.Nombre,
                TotalMedicamentosVendidos = g.Sum(x => x.Cantidad)
            }
        ).ToListAsync();

        return totalMedicamentosPorProveedor;
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

    public async Task<IEnumerable<Object>> GetInfoMedicamentoVencidos()
    {
        DateTime fechaInicio = new DateTime(2024, 1, 1);
        DateTime fechaFin = new DateTime(2024, 12, 31);

        var medicamentos = await _context.Medicamentos
        .Where(m => m.FechaVencimiento >= fechaInicio && m.FechaVencimiento <= fechaFin)
        .ToListAsync();

        return medicamentos;
    }

    public async Task<IEnumerable<object>> MedicamentosVendidosPorMesEn2023Async()
    {
        var resultados = await (
            from factura in _context.Facturas
            join detalleFactura in _context.DetalleFacturas on factura.Id equals detalleFactura.IdFacturaFk
            join medicamento in _context.Medicamentos on detalleFactura.IdMedicamentoFk equals medicamento.Id
            where factura.FechaCreacion.Year == 2023
            select new
            {
                medicamento.Id,
                medicamento.Nombre,
                factura.FechaCreacion
            }
        ).ToListAsync();

        var resultadosAgrupados = resultados
            .GroupBy(x => new { x.Id, x.Nombre, Mes = x.FechaCreacion.ToString("yyyy-MM") })
            .Select(g => new
            {
                g.Key.Id,
                g.Key.Nombre,
                Mes = g.Key.Mes,
                Total = g.Sum(x => 1)  
            })
            .ToList();

        return resultadosAgrupados;
    }
    public async Task<IEnumerable<object>> MedicamentosNoVendidosEn2023(int year)
    {
        var medicamentosNoVendidos = await (
            from m in _context.Medicamentos
            where !_context.DetalleFacturas
                .Join(
                    _context.Facturas.Where(f => f.FechaCreacion.Year == year),
                    df => df.IdFacturaFk,
                    f => f.Id,
                    (df, f) => df.IdMedicamentoFk
                )
                .Distinct()
                .Contains(m.Id)
            select new
            {
                MedicamentoId = m.Id,
                NombreMedicamento = m.Nombre
            }
        ).ToListAsync();

        return medicamentosNoVendidos;
    }
    public async Task<int> TotalMedicamentosVendidosPrimerTrimestre2023()
    {
        var inicioTrimestre = new DateTime(2023, 1, 1);
        var finTrimestre = new DateTime(2023, 3, 31);

        var total = await (
            from df in _context.DetalleFacturas
            join f in _context.Facturas on df.IdFacturaFk equals f.Id
            where f.FechaCreacion >= inicioTrimestre && f.FechaCreacion <= finTrimestre
            select df.Cantidad
        ).SumAsync();

        return total;
    }
    public async Task<IEnumerable<Medicamento>> MedicamentosConPrecioYStockAsync()
    {
        var medicamentos = await (
            from m in _context.Medicamentos
            where m.PrecioUnitario > 50 && m.Stock < 100
            select m
        ).ToListAsync();

        return medicamentos;
    }


    public async Task<int> TotalVentasParacetamolAsync()
    {
        var totalVentas = await (
            from df in _context.DetalleFacturas
            join m in _context.Medicamentos on df.IdMedicamentoFk equals m.Id
            where m.Nombre.ToUpper().Contains("PARACETAMOL")
            select df.Cantidad
        ).SumAsync();

        return totalVentas;
    }

    public async Task<IEnumerable<Medicamento>> MedicamentosVencidosAntesDe1Enero()
    {
        var medicamentosCaducados = await _context.Medicamentos
        .Where(m => m.FechaVencimiento < new DateTime(2024, 1, 1))
        .ToListAsync();

        return medicamentosCaducados;
    }

    public async Task<IEnumerable<Medicamento>> MedicamentosNoVendidosAsync()
    {
        var medicamentosNoVendidos = await (
            from m in _context.Medicamentos
            where !_context.DetalleFacturas.Any(df => df.IdMedicamentoFk == m.Id)
            select m
        ).ToListAsync();

        return medicamentosNoVendidos;
    }

    public async Task<Medicamento> ObtenerMedicamentoMasCaroAsync()
    {
        var medicamentoMasCaro = await _context.Medicamentos
            .OrderByDescending(m => m.PrecioUnitario)
            .FirstOrDefaultAsync();

        return medicamentoMasCaro;
    }

    
    public async Task<int> ObtenerTotalMedicamentosVendidosMarzo2023Async()
    {
        var totalMedicamentosVendidos = await (
            from df in _context.DetalleFacturas
            join f in _context.Facturas on df.IdFacturaFk equals f.Id
            where f.FechaCreacion >= new DateTime(2023, 3, 1) && f.FechaCreacion <= new DateTime(2023, 3, 31)
            select df.Cantidad
        ).SumAsync();

        return totalMedicamentosVendidos;
    }

    public async Task<object> ObtenerMedicamentoMenosVendido2023Async()
    {
        var medicamentoMenosVendido = await (
            from df in _context.DetalleFacturas
            join f in _context.Facturas on df.IdFacturaFk equals f.Id
            join m in _context.Medicamentos on df.IdMedicamentoFk equals m.Id
            where f.FechaCreacion >= new DateTime(2023, 1, 1) && f.FechaCreacion <= new DateTime(2023, 12, 31)
            group new { df, m } by new { df.IdMedicamentoFk, m.Nombre } into g
            orderby g.Sum(x => x.df.Cantidad) ascending
            select new
            {
                Id = g.Key.IdMedicamentoFk,
                NombreMedicamento = g.Key.Nombre,
                TotalVentas = g.Sum(x => x.df.Cantidad)
            }
        ).FirstOrDefaultAsync();

        return medicamentoMenosVendido;
    }
    
    public async Task<double> ObtenerPromedioMedicamentosPorVentaAsync()
    {
        var promedioMedicamentosPorVenta = await (
            from cp in _context.CompraProveedores
            join df in _context.DetalleFacturas on cp.IdMedicamentoFk equals df.IdMedicamentoFk
            select df.Cantidad
        ).AverageAsync();

        return promedioMedicamentosPorVenta;
    }

    public async Task<IEnumerable<Medicamento>> ObtenerMedicamentosExpiranEn2024Async()
    {
        var medicamentosExpiranEn2024 = await (
            from m in _context.Medicamentos
            where m.FechaVencimiento >= new DateTime(2024, 1, 1) && m.FechaVencimiento <= new DateTime(2024, 12, 31)
            select m
        ).ToListAsync();

        return medicamentosExpiranEn2024;
    }



}