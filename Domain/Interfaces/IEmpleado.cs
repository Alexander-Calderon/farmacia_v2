using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IEmpleado:IGenericRepository<Empleado>
    {
<<<<<<< HEAD
        Task<IEnumerable<Empleado>> ObtenerEmpleadosSinFacturasAsync();
        Task<IEnumerable<object>> ObtenerTotalVentas5PorEmpleadoAsync();
=======
        Task<IEnumerable<Object>> GetInfoCantidadVentas();
        Task<IEnumerable<Object>> GetEmpleadocon5Ventas();

>>>>>>> a9760254e668227d181f52a65af81084078ccc70
    }
