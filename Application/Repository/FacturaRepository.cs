
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class FacturaRepository : GenericRepository<Factura>, IFactura
{
    private readonly FarmaciaContext _context;

    public FacturaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Factura>> GetAllAsync()
    {
        return await _context.Facturas
        .Include(p => p.DetallesFacturas)
        .ToListAsync();
    }

    public override async Task<Factura> GetByIdAsync(int id)
    {
        return await _context.Facturas
        .Include(p => p.DetallesFacturas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}