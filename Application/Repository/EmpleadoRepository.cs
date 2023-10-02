
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

}