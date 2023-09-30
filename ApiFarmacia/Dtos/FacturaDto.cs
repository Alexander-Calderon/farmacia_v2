using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class FacturaDto:BaseEntity
    {
        public int IdEmpleadoFk { get; set; }
        public int IdPacienteFk { get; set; }
        public int IdEstadoFk { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
