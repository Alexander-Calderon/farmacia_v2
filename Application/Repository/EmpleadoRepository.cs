
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






}