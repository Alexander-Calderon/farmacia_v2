using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IPaciente:IGenericRepository<Paciente>
    {
        Task<IEnumerable<object>> ObtenerPacienteConMayorGastoAsync();
        Task<IEnumerable<Object>> GetInfoPacientesCompraMedicamento(int IdMedicamento);
        Task<IEnumerable<object>> ObtenerPacientesConParacetamolAsync();
        Task<IEnumerable<Paciente>> ObtenerPacientesSinComprasEn2023Async();
        Task<object> TotalGastadoPorPacienteEn2023Async();
    }
