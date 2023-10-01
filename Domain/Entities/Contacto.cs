using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Contacto  : BaseEntity
    {
        public string Descripcion {get; set;}


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdTipoContactoFk {get; set;}
        public TipoContacto TipoContacto {get;set;}

        

        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A CONTACTO (TABLAS QUE TIENEN DE FK AL ID DE CONTACTO).            
        */ 
        public ICollection<ContactoEmpleado> ContactosEmpleados {get; set;}
        public ICollection<ContactoProveedor> ContactosProveedores {get; set;}
    }
