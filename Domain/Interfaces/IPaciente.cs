using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IPaciente:IGenericRepository<Paciente>
    {
        Task<object> ObtenerPacienteQueMasGastoAsync();
        Task<IEnumerable<Object>> GetInfoPacientesCompraMedicamento(int IdMedicamento);
    }
