
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EstadoRepository : GenericRepository<Estado>, IEstado
{
    private readonly FarmaciaContext _context;

    public EstadoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Estado>> GetAllAsync()
    {
        return await _context.Estados
        .Include(p => p.Facturas)
        .ToListAsync();
    }

    public override async Task<Estado> GetByIdAsync(int id)
    {
        return await _context.Estados
        .Include(p => p.Facturas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}