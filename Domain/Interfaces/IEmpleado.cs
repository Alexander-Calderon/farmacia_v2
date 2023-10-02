using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IEmpleado:IGenericRepository<Empleado>
    {
        Task<IEnumerable<Empleado>> ObtenerEmpleadosSinFacturasAsync();
        Task<IEnumerable<object>> ObtenerTotalVentas5PorEmpleadoAsync();
        Task<IEnumerable<Object>> GetInfoCantidadVentas();
        Task<IEnumerable<Object>> GetEmpleadocon5Ventas();
        Task<object> EmpleadoMaxMedicamentosDistintos(int year);
        Task<IEnumerable<object>> EmpleadosSinVentasEnAbril2023Async();

    }
