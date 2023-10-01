using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Empleado : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaRegistro {get; set;}
        public string Documento {get; set;}


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdCargoFk {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public int IdDireccionFk {get; set;}
        public int IdUserFk {get; set;}
        

        // * Propiedades de navegabilidad
        public Cargo Cargo {get; set;}
        public TipoDocumento TipoDocumento {get; set;}
        public Direccion Direccion {get; set;}

        // ! Se debe establecer tanto aquí como en la entidad user para que la navegabilidad sea bidireccional
        // ! ya que por defecto el comportamiento es como el de uno a muchos y la navegabilidad ahí por defecto es unidireccional de user a empleado.
        public User User {get; set;} 


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A EMPLEADO (TABLAS QUE TIENEN DE FK AL ID DE EMPLEADO).
        */ 
        public ICollection<ContactoEmpleado> ContactosEmpleados {get;set;}
        public ICollection<Factura> Facturas {get; set;}
        public ICollection<CompraProveedor> ComprasProveedores {get; set;}


    }
