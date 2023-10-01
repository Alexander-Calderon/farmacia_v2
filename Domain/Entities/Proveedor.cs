using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Proveedor:BaseEntity
    {
        public string Nombre {get; set;}
        public string Documento {get; set;}


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdTipoDocumentoFk {get; set;}
        public int IdDireccionFk {get; set;}

        public TipoDocumento TipoDocumento {get; set;}
        public Direccion Direccion {get; set;}

        /*
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DIRECCION (TABLAS QUE TIENEN DE FK AL ID DE DIRECCION).
        */
        public ICollection<ContactoProveedor> ContactosProveedores {get;set;}
        public ICollection<CompraProveedor> ComprasProveedores {get; set;}


    }
