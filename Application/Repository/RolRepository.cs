
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly FarmaciaContext _context;

    public RolRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Roles
        .Include(p => p.Users)
        .ToListAsync();
    }

    public override async Task<Rol> GetByIdAsync(int id)
    {
        return await _context.Roles
        .Include(p => p.Users)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}