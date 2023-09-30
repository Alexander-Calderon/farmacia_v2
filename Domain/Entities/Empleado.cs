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

        public int IdCargoFk {get; set;}
        public Cargo Cargo {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public TipoDocumento TipoDocumento {get; set;}
        public int IdDireccionFk {get; set;}
        public Direccion Direccion {get; set;}
        public int IdContactoFk {get; set;}
        public Contacto Contacto {get; set;}

        public int IdUserFk {get; set;}
        public User User {get; set;}

        public ICollection<Factura> Facturas {get; set;}

        public ICollection<CompraProveedor> CompraProveedores {get; set;}


    }
