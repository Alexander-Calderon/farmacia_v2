
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
}