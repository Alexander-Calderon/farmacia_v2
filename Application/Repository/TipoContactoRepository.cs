
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TipoContactoRepository : GenericRepository<TipoContacto>, ITipoContacto
{
    private readonly FarmaciaContext _context;

    public TipoContactoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoContacto>> GetAllAsync()
    {
        return await _context.TipoContactos
        .Include(p => p.ContactoDetalles)
        .ToListAsync();
    }

    public override async Task<TipoContacto> GetByIdAsync(int id)
    {
        return await _context.TipoContactos
        .Include(p => p.ContactoDetalles)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}