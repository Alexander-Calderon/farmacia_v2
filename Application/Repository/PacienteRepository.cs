
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PacienteRepository : GenericRepository<Paciente>, IPaciente
{
    private readonly FarmaciaContext _context;

    public PacienteRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> GetInfoPacientesCompraMedicamento(int IdMedicamento)
    {
        return await 
        (
            from p in _context.Pacientes
            join f in _context.Facturas on p.Id equals f.Id
            join df in _context.DetalleFacturas on f.Id equals df.Id
            where df.Id == IdMedicamento
            select p
        ).Distinct().ToListAsync();

    }
}