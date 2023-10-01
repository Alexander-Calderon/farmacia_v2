using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class ContactoProveedor
    {


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdContactoFk {get; set;}
        public int IdProveedorFk {get; set;}
        
        public Contacto Contacto   {get; set;}
        public Proveedor Proveedor {get; set;}
    }
