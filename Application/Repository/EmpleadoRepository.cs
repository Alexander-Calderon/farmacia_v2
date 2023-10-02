
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
{
    private readonly FarmaciaContext _context;

    public EmpleadoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
        .Include(p => p.Facturas)
        .Include(p => p.ComprasProveedores)
        .ToListAsync();
    }

    public override async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
        .Include(p => p.Facturas)
        .Include(p => p.ComprasProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Empleado>> ObtenerEmpleadosSinFacturasAsync()
    {
        var empleadosSinFacturas = await (
            from e in _context.Empleados
            join f in _context.Facturas
                on e.Id equals f.IdEmpleadoFk into facturasGroup
            where facturasGroup.All(f => f == null || f.FechaCreacion < new DateTime(2023, 01, 01))
            select e
        ).ToListAsync();

        return empleadosSinFacturas;
    }

    public async Task<IEnumerable<object>> ObtenerTotalVentas5PorEmpleadoAsync()
{
    var totalVentas2023PorEmpleado = await (
        from e in _context.Empleados
        select new
        {
            IdEmpleado = e.Id,
            NombreEmpleado = e.Nombre,
            TotalVentas2023 = _context.Facturas
                .Join(
                    _context.DetalleFacturas,
                    f => f.Id,
                    df => df.IdFacturaFk,
                    (f, df) => new { Factura = f, DetalleFactura = df }
                )
                .Where(joined => joined.Factura.FechaCreacion.Year == 2023 && joined.Factura.IdEmpleadoFk == e.Id)
                .Count()
        }
    ).ToListAsync();

    return totalVentas2023PorEmpleado;
}





    public async Task<IEnumerable<Object>> GetInfoCantidadVentas()
    {
        var result = await (
            from df in _context.DetalleFacturas
            join f in _context.Facturas on df.IdFacturaFk equals f.Id
            where f.FechaCreacion >= new DateTime(2023, 1, 1)
            group df by df.IdMedicamentoFk into grouped
            select new
            {
                IdEmpleadoFK = grouped.Key,
                TotalVentas = grouped.Count()
            }
        ).ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Object>> GetEmpleadocon5Ventas()
    {
        var empleadosConVentas = await 
        (
            from f in _context.Facturas
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            where f.FechaCreacion >= new DateTime(2023, 1, 1)
            select new
            {
                IdEmpleadoFK = f.IdEmpleadoFk,
            }
        ).ToListAsync();

        var empleadosConMasDe5Ventas = empleadosConVentas
            .GroupBy(e => e.IdEmpleadoFK)
            .Where(group => group.Count() > 5)
            .Select(group => new
            {
                IdEmpleadoFK = group.Key,
                TotalVentas = group.Count()
            });

        return empleadosConMasDe5Ventas;
    }
    public async Task<object> EmpleadoMaxMedicamentosDistintos(int year)
    {
        var inicioAño = new DateOnly(year, 1, 1);
        var finAño = new DateOnly(year, 12, 31);

        var resultados = await (
            from e in _context.Empleados
            join f in _context.Facturas on e.Id equals f.IdEmpleadoFk
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            join m in _context.Medicamentos on df.IdMedicamentoFk equals m.Id
            where f.FechaCreacion.Year == 2023
            group m by new { e.Id, e.Nombre } into grupoVentas
            orderby grupoVentas.Select(x => x.Id).Count() descending
            select new
            {
                IdEmpleado = grupoVentas.Key.Id,
                Empleado = grupoVentas.Key.Nombre,
                TotalMedicamentosDistintos = grupoVentas.Select(x => x.Id).Count(),
                MedicamentosVendidos = string.Join(", ", grupoVentas.Select(x => x.Nombre).Distinct())
            }
        ).FirstOrDefaultAsync();

        return resultados;
    }
    public async Task<IEnumerable<object>> EmpleadosSinVentasEnAbril2023Async()
    {
        var inicioAbril = new DateOnly(2023, 4, 1);  // Fecha de inicio de abril
        var finAbril = new DateOnly(2023, 4, 30);    // Fecha de fin de abril

        // Convierte DateOnly a DateTime con horas específicas
        var inicioAbrilDateTime = new DateTime(inicioAbril.Year, inicioAbril.Month, inicioAbril.Day, 0, 0, 0);  // Hora de inicio del día
        var finAbrilDateTime = new DateTime(finAbril.Year, finAbril.Month, finAbril.Day, 23, 59, 59);    // Hora de finalización del día

        var empleadosSinVentasAbril = await (
            from e in _context.Empleados
            join f in _context.Facturas on e.Id equals f.IdEmpleadoFk into ventas
            from venta in ventas.DefaultIfEmpty()
            where venta == null || (venta.FechaCreacion < inicioAbrilDateTime || venta.FechaCreacion > finAbrilDateTime)
            group e by new { e.Id, e.Nombre } into grupoEmpleados
            select new
            {
                IdEmpleado = grupoEmpleados.Key.Id,
                NombreEmpleado = grupoEmpleados.Key.Nombre,
                FechasVentas = grupoEmpleados.Select(e => e.FechaRegistro).Distinct().OrderBy(fecha => fecha).ToList()
            }
        ).ToListAsync();

        return empleadosSinVentasAbril;

    }

    public async Task<IEnumerable<object>> ObtenerCantidadVentasPorEmpleadoAsync()
    {
        var ventasPorEmpleado = await (
            from e in _context.Empleados
            join f in _context.Facturas on e.Id equals f.IdEmpleadoFk
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            where f.FechaCreacion >= new DateTime(2023, 1, 1) && f.FechaCreacion <= new DateTime(2023, 12, 31)
            group e by new { e.Id, e.Nombre } into g
            select new
            {
                IdEmpleado = g.Key.Id,
                NombreEmpleado = g.Key.Nombre,
                TotalVentas = g.Count()
            }
        ).ToListAsync();

        return ventasPorEmpleado;
    }

    public async Task<IEnumerable<object>> ObtenerEmpleadosConMasDe5VentasAsync()
    {
        var empleadosConMasDe5Ventas = await (
            from e in _context.Empleados
            join f in _context.Facturas on e.Id equals f.IdEmpleadoFk
            join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
            group e by new { e.Id, e.Nombre } into g
            where g.Count() > 5
            select new
            {
                IdEmpleado = g.Key.Id,
                NombreEmpleado = g.Key.Nombre,
                TotalVentas = g.Count()
            }
        ).ToListAsync();

        return empleadosConMasDe5Ventas;
    }

}