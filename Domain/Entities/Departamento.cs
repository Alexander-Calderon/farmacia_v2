using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Departamento: BaseEntity  
    {
        public string Nombre {get; set;}

        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdPaisFk {get; set;}

        public Pais Pais {get; set;}

        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DEPARTAMENTO (TABLAS QUE TIENEN DE FK AL ID DE DEPARTAMENTO).
        */ 
        public ICollection<Ciudad> Ciudades {get; set;}
    }
}



