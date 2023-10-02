
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync();
    }
