using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Medicamento:BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public DateTime FechaVencimiento { get; set; }


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdCategoriaFk { get; set; }
        public int IdTipoPresentacionFk { get; set; }
        public int IdMarcaFk { get; set; }

        public Categoria Categoria { get; set; }
        public TipoPresentacion TipoPresentacion { get; set; }
        public Marca Marca { get; set; }


        /*
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A MEDICAMENTO (TABLAS QUE TIENEN DE FK AL ID DE MEDICAMENTO).
        */
        public ICollection<CompraProveedor> ComprasProveedores { get; set; }
        public ICollection<DetalleFactura> DetallesFacturas { get; set; }
        
    }
