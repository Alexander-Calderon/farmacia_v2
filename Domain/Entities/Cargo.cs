using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Cargo:BaseEntity
    {
        public string Nombre {get; set;}


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A CARGO (TABLAS QUE TIENEN DE FK AL ID DE CARGO).            
        */
        public ICollection<Empleado> Empleados {get; set;}
    }
