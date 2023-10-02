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
<<<<<<< HEAD
        Task<IEnumerable<object>> ObtenerProveedorConMasMedicamentosSuministradosAsync();
        Task<int> ObtenerTotalProveedoressuminitroAsync();
        Task<IEnumerable<object>> ObtenerProveedoresConStockBajoAsync(int stockMinimo);
=======
        Task<IEnumerable<Object>> GetInfoGananciaPorProveedor();

>>>>>>> a9760254e668227d181f52a65af81084078ccc70

    }
