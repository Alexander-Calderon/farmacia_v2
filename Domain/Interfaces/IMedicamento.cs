
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync();
        Task<IEnumerable<Object>> GetInfoMedicamentoVendidos();
        Task<IEnumerable<Object>> GetInfoMedicamentoMenosVendido();
        Task<IEnumerable<object>> ObtenerTotalMedicamentosVendidosPorMesEn2023Async();
        
    }
