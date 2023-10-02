
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        // * 1 Obtener todos los medicamentos con menos de 50 unidades en stock.
        Task<IEnumerable<Medicamento>> GetCantidadMenorA50();

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

        Task<int> TotalVentasParacetamolAsync();
        Task<IEnumerable<Medicamento>> MedicamentosVencidosAntesDe1Enero();

        


    }
