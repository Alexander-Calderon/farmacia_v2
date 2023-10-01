
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
}