
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EspecialidadRepository : GenericRepository<Especialidad>, IEspecialidad
{
    private readonly FarmaciaContext _context;

    public EspecialidadRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Especialidad>> GetAllAsync()
    {
        return await _context.Especialidades
        .Include(p => p.Doctores)
        .ToListAsync();
    }

    public override async Task<Especialidad> GetByIdAsync(int id)
    {
        return await _context.Especialidades
        .Include(p => p.Doctores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}