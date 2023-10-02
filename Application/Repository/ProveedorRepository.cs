
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
        return await
        (
        from cp in _context.CompraProveedores
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        group cp by new { p.Nombre, cp.IdProveedorFk } into grouped
        select new
        {
            NombreProveedor = grouped.Key.Nombre,
            IdProveedorFK = grouped.Key.IdProveedorFk,
            Cantidad = grouped.Count()
        }
        ).ToListAsync();
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

    public async Task<IEnumerable<object>> ObtenerProveedorConMasMedicamentosSuministradosAsync()
    {
        var proveedorConMasMedicamentosSuministrados = await (
            from p in _context.Proveedores
            join cp in _context.CompraProveedores on p.Id equals cp.IdProveedorFk
            where cp.FechaCompra >= new DateTime(2023, 01, 01) && cp.FechaCompra <= new DateTime(2023, 12, 31)
            group cp by new { p.Id, p.Nombre } into g
            orderby g.Sum(cp => cp.Cantidad) descending
            select new
            {
                Id = g.Key.Id,
                ProveedorNombre = g.Key.Nombre,
                TotalMedicamentosSuministrados = g.Sum(cp => cp.Cantidad)
            }
        ).FirstOrDefaultAsync();

        return new List<object> { proveedorConMasMedicamentosSuministrados };
    }

    public async Task<int> ObtenerTotalProveedoressuminitroAsync()
    {
        var totalProveedores = await (
            from p in _context.Proveedores
            join cp in _context.CompraProveedores on p.Id equals cp.IdProveedorFk
            where cp.FechaCompra >= new DateTime(2023, 1, 1)
            select p.Id
        ).Distinct().CountAsync();

        return totalProveedores;
    }


    public async Task<IEnumerable<object>> ObtenerProveedoresConStockBajoAsync(int stockMinimo)
{
    var proveedoresConStockBajo = await (
        from m in _context.Medicamentos
        join cp in _context.CompraProveedores on m.Id equals cp.IdMedicamentoFk
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        where m.Stock < stockMinimo
        select new 
        {
            Proveedor = p,
            Stock = m.Stock
        }
    ).Distinct().ToListAsync();

    return proveedoresConStockBajo;
}




}