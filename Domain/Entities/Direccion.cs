using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Direccion :BaseEntity
    {
        public string Descripcion {get; set;}


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdCiudadFk {get; set;}

        public Ciudad Ciudad {get; set;}


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DIRECCION (TABLAS QUE TIENEN DE FK AL ID DE DIRECCION).
        */ 
        public ICollection<Proveedor> Proveedores {get; set;}
        public ICollection<Empleado> Empleados {get; set;}        

    }
