
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        Task<IEnumerable<Object>> GetInfoMedicamentoVendidos();
        Task<IEnumerable<Object>> GetInfoMedicamentoMenosVendido();
        Task<IEnumerable<Object>> GetInfoPromedioMedicamento();
        Task<IEnumerable<Object>> GetInfoMedicamentoVencidos();



    }
