
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        // * 1 Obtener todos los medicamentos con menos de 50 unidades en stock.
        Task<IEnumerable<Medicamento>> GetCantidadMenorA50();

        
    }
