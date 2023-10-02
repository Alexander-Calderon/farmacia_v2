using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IProveedor:IGenericRepository<Proveedor>
    {
        Task<IEnumerable<Object>> GetInfoMedicamentoPorProveedor();
        Task<IEnumerable<Object>> GetInfoVentaUltimoAnoProveedor();
        Task<IEnumerable<object>> ObtenerProveedorConMasMedicamentosSuministradosAsync();
        Task<int> ObtenerTotalProveedoressuminitroAsync();
        Task<IEnumerable<object>> ObtenerProveedoresConStockBajoAsync(int stockMinimo);
        Task<IEnumerable<Object>> GetInfoGananciaPorProveedor();
        Task<IEnumerable<object>> ProveedoresConCincoMedicamentosDiferentesEn2023();
        Task<IEnumerable<object>> ProveedoresConInformacionDeContactoAsync();
        Task<IEnumerable<object>> MedicamentosCompradosPorProveedorAsync(string proveedorNombre);

        Task<IEnumerable<object>> ObtenerGananciaTotalPorProveedor2023Async();
    }
