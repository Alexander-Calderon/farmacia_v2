
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
    public async Task<IEnumerable<object>> ProveedoresConCincoMedicamentosDiferentesEn2023()
{
    var inicioAño = new DateTime(2023, 1, 1);
    var finAño = new DateTime(2023, 12, 31);

    var resultados = await (
        from cp in _context.CompraProveedores
        join m in _context.Medicamentos on cp.IdMedicamentoFk equals m.Id
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        where cp.FechaCompra >= inicioAño && cp.FechaCompra <= finAño
        group m by new { p.Id, p.Nombre } into grupoProveedores
        where grupoProveedores.Count() >= 5
        select new
        {
            ProveedorId = grupoProveedores.Key.Id,
            NombreProveedor = grupoProveedores.Key.Nombre,
            TotalMedicamentos = grupoProveedores.Count(),
            Medicamentos = string.Join(", ", grupoProveedores.Select(med => med.Nombre).OrderBy(n => n))
        }
    ).ToListAsync();

    return resultados;
}

public async Task<IEnumerable<object>> ProveedoresConInformacionDeContactoAsync()
{
    var resultados = await (
        from m in _context.Medicamentos
        join cp in _context.CompraProveedores on m.Id equals cp.IdMedicamentoFk
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        join cpv in _context.ContactoProveedores on p.Id equals cpv.IdProveedorFk into contactosProveedor
        from contactoProveedor in contactosProveedor.DefaultIfEmpty()
        join c in _context.Contactos on contactoProveedor.IdContactoFk equals c.Id into contactos
        from contacto in contactos.DefaultIfEmpty()
        join tc in _context.TipoContactos on contacto.IdTipoContactoFk equals tc.Id into tiposContactos
        from tipoContacto in tiposContactos.DefaultIfEmpty()
        group new { m, p, contacto, tipoContacto } by new { m.Id, m.Nombre, NombreProveedor = p.Nombre } into grupoProveedores
        select new
        {
            IdMedicamento = grupoProveedores.Key.Id,
            NombreMedicamento = grupoProveedores.Key.Nombre,
            NombreProveedor = grupoProveedores.Key.NombreProveedor,
            ContactosProveedor = string.Join(", ", grupoProveedores.Where(x => x.contacto != null).Select(x => x.contacto.Descripcion).Distinct()),
            TiposContactoProveedor = string.Join(", ", grupoProveedores.Where(x => x.tipoContacto != null).Select(x => x.tipoContacto.Nombre).Distinct())
        }
    ).ToListAsync();

    return resultados;
}

public async Task<IEnumerable<object>> MedicamentosCompradosPorProveedorAsync(string proveedorNombre)
{
    var resultados = await (
        from m in _context.Medicamentos
        join cp in _context.CompraProveedores on m.Id equals cp.IdMedicamentoFk
        join p in _context.Proveedores on cp.IdProveedorFk equals p.Id
        where p.Nombre.Contains(proveedorNombre) // Utiliza Contains para buscar proveedores que contengan el nombre dinámico.
        select new
        {
            IdMedicamento = m.Id,
            NombreMedicamento = m.Nombre
        }
    ).ToListAsync();

    return resultados;
}


}