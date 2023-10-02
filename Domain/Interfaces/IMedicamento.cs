
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync();
        Task<IEnumerable<Object>> GetInfoMedicamentoVendidos();
        Task<IEnumerable<Object>> GetInfoMedicamentoMenosVendido();
        Task<IEnumerable<object>> ObtenerTotalMedicamentosVendidosPorMesEn2023Async();
        Task<IEnumerable<Object>> GetInfoPromedioMedicamento();
        Task<IEnumerable<Object>> GetInfoMedicamentoVencidos();
        Task<IEnumerable<object>> MedicamentosVendidosPorMesEn2023Async();
        Task<IEnumerable<object>> MedicamentosNoVendidosEn2023(int year);
        Task<int> TotalMedicamentosVendidosPrimerTrimestre2023();
        Task<IEnumerable<Medicamento>> MedicamentosConPrecioYStockAsync();


    }
