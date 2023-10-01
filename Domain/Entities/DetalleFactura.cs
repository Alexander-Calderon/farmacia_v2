using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class DetalleFactura : BaseEntity
    {
        public decimal PrecioUnitario {get; set;}
        public int Cantidad {get; set;}

        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdFacturaFk { get; set; }
        public int IdMedicamentoFk { get; set; }
        public int IdRecetaFk { get; set; }


        public Factura Factura { get; set; }
        public Medicamento Medicamento { get; set; }
        public Receta Receta { get; set; }

    }
