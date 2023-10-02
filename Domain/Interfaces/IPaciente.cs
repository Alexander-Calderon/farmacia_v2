
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IPaciente:IGenericRepository<Paciente>
    {
        Task<IEnumerable<object>> GetInfoPacientesCompraMedicamento();
        Task<IEnumerable<object>> ObtenerPacienteConMayorGastoAsync();
        Task<IEnumerable<object>> ObtenerPacientesConParacetamolAsync();
        Task<IEnumerable<Paciente>> ObtenerPacientesSinComprasEn2023Async();
        Task<object> TotalGastadoPorPacienteEn2023Async();
    }
