using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Especializacion:BaseEntity
    {
        public string Nombre {get; set;}
        public string Descripcion {get; set;}


       /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A ESPECIALIZACION (TABLAS QUE TIENEN DE FK AL ID DE ESPECIALIZACION).
        */ 
        public ICollection<Doctor> Doctores {get; set;}
    }
