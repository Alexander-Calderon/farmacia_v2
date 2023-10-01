
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

namespace Application.Repository;
    public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
            {
                private readonly FarmaciaContext _context;
            
                public CiudadRepository(FarmaciaContext context) : base(context)
                {
                    _context = context;
                }
            
                public override async Task<IEnumerable<Ciudad>> GetAllAsync()
                    {
                        return await _context.Ciudades
                        .Include(p => p.Direcciones)
                        .ToListAsync();
                    }
            
                    public override async Task<Ciudad> GetByIdAsync(int id)
                    {
                        return await _context.Ciudades
                        .Include(p => p.Direcciones)
                        .FirstOrDefaultAsync(p => p.Id == id);
                    }
            }