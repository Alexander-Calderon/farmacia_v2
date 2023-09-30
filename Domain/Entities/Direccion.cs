using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Direccion :BaseEntity
    {
        public string Descripcion {get; set;}

        public int IdCiudadFk {get; set;}

        public ICollection<Proveedor> Proveedores {get; set;}
        public ICollection<Empleado> Empleados {get; set;}
        public ICollection<Paciente> Pacientes {get; set;}

    }
