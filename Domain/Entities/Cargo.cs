using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Cargo:BaseEntity
    {
        public string Nombre {get; set;}

        public ICollection<Empleado> Empleados {get; set;}
    }
