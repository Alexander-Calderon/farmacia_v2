
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ContactoRepository : GenericRepository<Contacto>, IContacto
{
    private readonly FarmaciaContext _context;

    public ContactoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Contacto>> GetAllAsync()
    {
        return await _context.Contactos
        .Include(p => p.ContactosEmpleados)
        .Include(p => p.ContactosProveedores)
        .ToListAsync();
    }

    public override async Task<Contacto> GetByIdAsync(int id)
    {
        return await _context.Contactos
        .Include(p => p.ContactosEmpleados)
        .Include(p => p.ContactosProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}