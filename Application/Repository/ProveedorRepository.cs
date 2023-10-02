
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
{
    private readonly FarmaciaContext _context;

    public ProveedorRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores
        .Include(p => p.ComprasProveedores)
        .ToListAsync();
    }

    public override async Task<Proveedor> GetByIdAsync(int id)
    {
        return await _context.Proveedores
        .Include(p => p.ComprasProveedores)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Object>> GetInfoMedicamentoPorProveedor()
    {
        var query = from p in _context.Proveedores
                    join c in _context.CompraProveedores
                    on p.Id equals c.IdProveedorFk into purchases
                    from purchase in purchases.DefaultIfEmpty()
                    group purchase by new { p.Id, p.Nombre } into grouped
                    orderby grouped.Count(g => g != null) descending
                    select new
                    {
                        Proveedor = grouped.Key.Nombre,
                        NumeroDeMedicamentos = grouped.Count(g => g != null)
                    };

        var result = await query.ToListAsync(); 

        return result;
    }


    public async Task<IEnumerable<Object>> GetInfoVentaUltimoAnoProveedor()
    {
        return await 
        (
            from cp in _context.CompraProveedores
            join df in _context.DetalleFacturas on cp.IdEmpleadoFk equals df.Id into joinedDf
            from subDf in joinedDf.DefaultIfEmpty()

            join f in _context.Facturas on subDf.IdFacturaFk equals f.Id into joinedFacturas
            from subFactura in joinedFacturas.DefaultIfEmpty()
            where subFactura == null || subFactura.FechaCreacion < DateTime.Now.AddYears(-1)
            select new 
            {
                IdProveedor = cp.IdProveedorFk,
                NombreProveedor = cp.Proveedor.Nombre 
            }
        ).Distinct().ToListAsync();
    }
    public async Task<IEnumerable<Object>> GetInfoGananciaPorProveedor()
    {
        var result = await (
            from cp in _context.CompraProveedores
            join df in _context.DetalleFacturas on cp.IdMedicamentoFk equals df.Id
            join f in _context.Facturas on df.IdFacturaFk equals f.Id
            where f.FechaCreacion >= new DateTime(2023, 1, 1)
            group new { cp, df } by cp.IdProveedorFk into grouped
            select new
            {
                IdProveedorFK = grouped.Key,
                Ganancia = grouped.Sum(item => item.df.Cantidad * (item.df.PrecioUnitario - item.cp.PrecioUnitario))
            }
        ).ToListAsync();

        return result;
    }


}