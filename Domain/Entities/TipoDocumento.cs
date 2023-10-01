using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class TipoDocumento:BaseEntity   
    {
        public string Nombre { get; set; }


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DIRECCION (TABLAS QUE TIENEN DE FK AL ID DE DIRECCION).
        */
        public ICollection<Paciente> Pacientes { get; set;}
        public ICollection<Empleado> Empleados { get; set;}
        public ICollection<Proveedor> Proveedores {get; set;}
        public ICollection<Doctor> Doctores{ get; set;}
    }
