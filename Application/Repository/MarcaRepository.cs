
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

namespace Application.Repository;
    public class MarcaRepository : GenericRepository<Marca>, IMarca
            {
                private readonly FarmaciaContext _context;
            
                public MarcaRepository(FarmaciaContext context) : base(context)
                {
                    _context = context;
                }
            
                public override async Task<IEnumerable<Marca>> GetAllAsync()
                    {
                        return await _context.Marcas
                        .Include(p => p.Medicamentos)
                        .ToListAsync();
                    }
            
                    public override async Task<Marca> GetByIdAsync(int id)
                    {
                        return await _context.Marcas
                        .Include(p => p.Medicamentos)
                        .FirstOrDefaultAsync(p => p.Id == id);
                    }
            }