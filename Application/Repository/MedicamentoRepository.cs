
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
{
    private readonly FarmaciaContext _context;

    public MedicamentoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Medicamento>> GetAllAsync()
    {
        return await _context.Medicamentos
        .Include(p => p.DetallesFacturas)
        .Include(p => p.ComprasProveedores)
        .ToListAsync();
    }

    public override async Task<Medicamento> GetByIdAsync(int id)
    {
        return await _context.Medicamentos
        .Include(p => p.DetallesFacturas)
        .Include(p => p.ComprasProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }




    public async Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync()
    {
        var medicamentosNoVendidos = await (
            from m in _context.Medicamentos
            where !_context.DetalleFacturas.Any(df => df.IdMedicamentoFk == m.Id)
            select new
            {
                Id = m.Id,
                Nombre = m.Nombre,
                // Agrega aquí otras propiedades que quieras seleccionar de Medicamentos
            }).ToListAsync();

        return medicamentosNoVendidos;
    }
}