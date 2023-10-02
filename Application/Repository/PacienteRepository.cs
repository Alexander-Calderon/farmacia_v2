
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


public async Task<IEnumerable<object>> ObtenerPacienteConMayorGastoAsync()
{
    var pacienteConMayorGasto = await (
        from p in _context.Pacientes
        join f in _context.Facturas on p.Id equals f.IdPacienteFk
        join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
        where f.FechaCreacion.Year == 2023
        group df by new { p.Id, p.Nombre } into g
        orderby g.Sum(df => df.PrecioUnitario * df.Cantidad) descending
        select new
        {
            Id = g.Key.Id,
            PacienteNombre = g.Key.Nombre,
            TotalGastado = g.Sum(df => df.PrecioUnitario * df.Cantidad)
        }
    ).FirstOrDefaultAsync();

    return new List<object> { pacienteConMayorGasto };
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


    public async Task<IEnumerable<object>> ObtenerPacientesConParacetamolAsync()
{
    var pacientesConParacetamol = await (
        from p in _context.Pacientes
        join f in _context.Facturas on p.Id equals f.IdPacienteFk
        join df in _context.DetalleFacturas on f.Id equals df.IdFacturaFk
        join m in _context.Medicamentos on df.IdMedicamentoFk equals m.Id
        where m.Nombre == "Paracetamol" && f.FechaCreacion.Year == 2023
        select new
        {
            Id = p.Id,
            PacienteNombre = p.Nombre
        }
    ).Distinct().ToListAsync();

    return pacientesConParacetamol;
}

public async Task<IEnumerable<Paciente>> ObtenerPacientesSinComprasEn2023Async()
{
    var pacientesSinComprasEn2023 = await (
        from p in _context.Pacientes
        join f in _context.Facturas on p.Id equals f.IdPacienteFk into facturaGroup
        from factura in facturaGroup.DefaultIfEmpty()
        join df in _context.DetalleFacturas on factura.Id equals df.IdFacturaFk into detalleFacturaGroup
        from detalleFactura in detalleFacturaGroup.DefaultIfEmpty()
        join m in _context.Medicamentos on detalleFactura.IdMedicamentoFk equals m.Id into medicamentoGroup
        from medicamento in medicamentoGroup.DefaultIfEmpty()
        where (factura == null || factura.FechaCreacion.Year != 2023) && medicamento == null
        select p
    ).Distinct().ToListAsync();

    return pacientesSinComprasEn2023;
}
public async Task<object> TotalGastadoPorPacienteEn2023Async()
{
    var inicioAño = new DateOnly(2023, 1, 1);
    var finAño = new DateOnly(2023, 12, 31);

    var resultados = await (
        from p in _context.Pacientes
        join f in _context.Facturas on p.Id equals f.IdPacienteFk into pacienteFacturas
        from pf in pacienteFacturas.DefaultIfEmpty()
        join df in _context.DetalleFacturas on pf.Id equals df.IdFacturaFk into facturaDetalles
        from dfd in facturaDetalles.DefaultIfEmpty()
        where pf.FechaCreacion.Year == 2023
        group new { p, dfd } by new { p.Id, p.Nombre } into grupoGastos
        select new
        {
            IdPaciente = grupoGastos.Key.Id,
            NombrePaciente = grupoGastos.Key.Nombre,
            TotalGastado = grupoGastos.Sum(x => x.dfd.Cantidad * x.dfd.PrecioUnitario)
        }
    ).ToListAsync();

    return resultados;
}


}