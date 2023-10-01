using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Doctor:BaseEntity
    {
        public string Nombre {get; set;}
        public DateTime FechaRegistro {get; set;}
        public string Documento {get; set;}

        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdTipoDocumentoFk {get; set;}
        public int IdEspecialidadFk {get; set;}

        public TipoDocumento TipoDocumento {get; set;}
        public Especializacion Especializacion {get; set;}

        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DOCTOR (TABLAS QUE TIENEN DE FK AL ID DE DOCTOR).
        */ 
        public ICollection<Receta> Recetas {get; set;}

    }
