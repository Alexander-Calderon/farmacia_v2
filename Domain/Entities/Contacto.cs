using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Contacto
    {
        public string Descripcion {get; set;}
        public ICollection<ContactoDetalle> ContactoDetalles {get; set;}
        public ICollection<Proveedor> Proveedores {get; set;}
        public ICollection<Empleado> Empleados {get; set;}
    }
