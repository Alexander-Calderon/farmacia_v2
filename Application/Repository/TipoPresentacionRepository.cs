
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TipoPresentacionRepository : GenericRepository<TipoPresentacion>, ITipoPresentacion
{
    private readonly FarmaciaContext _context;

    public TipoPresentacionRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoPresentacion>> GetAllAsync()
    {
        return await _context.TipoPresentaciones
        .Include(p => p.Medicamentos)
        .ToListAsync();
    }

    public override async Task<TipoPresentacion> GetByIdAsync(int id)
    {
        return await _context.TipoPresentaciones
        .Include(p => p.Medicamentos)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}