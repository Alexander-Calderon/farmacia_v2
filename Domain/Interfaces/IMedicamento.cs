using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IMedicamento:IGenericRepository<Medicamento>
    {
        // * 1 Obtener todos los medicamentos con menos de 50 unidades en stock.
        Task<IEnumerable<Medicamento>> GetCantidadMenorA50();

        
    }
