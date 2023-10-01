
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
}