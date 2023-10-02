
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


public async Task<object> ObtenerPacienteQueMasGastoAsync()
{
    var query = await (
        from df in _context.DetalleFacturas
        join d in _context.Facturas on df.IdFacturaFk equals d.Id
        where d.FechaCreacion >= new DateTime(2023, 1, 1)
        group df by d.IdPacienteFk into pacienteGroup
        let totalGasto = pacienteGroup.Sum(df => df.Cantidad * df.PrecioUnitario)
        orderby totalGasto descending
        select new
        {
            IdPaciente = pacienteGroup.Key,
            TotalGasto = totalGasto
        }
    ).FirstOrDefaultAsync();

    return query;
}

}