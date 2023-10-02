
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
{
    private readonly FarmaciaContext _context;

    public ProveedorRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores
        .Include(p => p.ComprasProveedores)
        .ToListAsync();
    }

    public override async Task<Proveedor> GetByIdAsync(int id)
    {
        return await _context.Proveedores
        .Include(p => p.ComprasProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Object>> GetInfoMedicamentoPorProveedor()
    {
        return await 
        (
        from cp in _context.CompraProveedores
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        group cp by new { p.Nombre, cp.IdProveedorFk } into grouped
            select new
            {
                NombreProveedor = grouped.Key.Nombre,
                IdProveedorFK = grouped.Key.IdProveedorFk,
                Cantidad = grouped.Count()
            }
        ).ToListAsync();
    }
}