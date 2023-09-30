using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Factura:BaseEntity
    {
        public int IdEmpleadoFk { get; set; }
        public Empleado Empleado { get; set; }

        public int IdPacienteFk { get; set; }
        public Paciente Paciente { get; set; }

        public int IdEstadoFk { get; set; }
        public Estado Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        
        public ICollection<DetalleFactura> DetalleFacturas { get; set; }
        
    }
