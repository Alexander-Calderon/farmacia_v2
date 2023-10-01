

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class CargoRepository : GenericRepository<Cargo>, ICargo
        {
            private readonly FarmaciaContext _context;
        
            public CargoRepository(FarmaciaContext context) : base(context)
            {
                _context = context;
            }
        
            public override async Task<IEnumerable<Cargo>> GetAllAsync()
                {
                    return await _context.Cargos
                    .Include(p => p.Empleados)
                    .ToListAsync();
                }
        
                public override async Task<Cargo> GetByIdAsync(int id)
                {
                    return await _context.Cargos
                    .Include(p => p.Empleados)
                    .FirstOrDefaultAsync(p => p.Id == id);
                }
        }