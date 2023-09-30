using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class TipoDocumento:BaseEntity   
    {
        public string Nombre { get; set; }

        public ICollection<Paciente> Pacientes { get; set;}
        public ICollection<Empleado> Empleados { get; set;}
        public ICollection<Proveedor> Proveedores {get; set;}
    }
