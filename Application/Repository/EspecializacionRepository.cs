
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EspecializacionRepository : GenericRepository<Especializacion>, IEspecializacion
{
    private readonly FarmaciaContext _context;

    public EspecializacionRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Especializacion>> GetAllAsync()
    {
        return await _context.Especializaciones
        .Include(p => p.Doctores)
        .ToListAsync();
    }

    public override async Task<Especializacion> GetByIdAsync(int id)
    {
        return await _context.Especializaciones
        .Include(p => p.Doctores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}