
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    

namespace Application.Repository;
    public class PaisRepository: GenericRepository<Pais>, IPais
            {
                private readonly FarmaciaContext _context;
            
                public PaisRepository(FarmaciaContext context) : base(context)
                {
                    _context = context;
                }
            
                public override async Task<IEnumerable<Pais>> GetAllAsync()
                    {
                        return await _context.Paises
                        .Include(p => p.Departamentos)
                        .ToListAsync();
                    }
            
                    public override async Task<Pais> GetByIdAsync(int id)
                    {
                        return await _context.Paises
                        .Include(p => p.Departamentos)
                        .FirstOrDefaultAsync(p => p.Id == id);
                    }
            }