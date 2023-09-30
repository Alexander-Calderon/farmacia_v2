using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class DetalleFactura : BaseEntity
    {
        public int IdFacturaFk { get; set; }
        public Factura Factura { get; set; }
        public int IdRecetaFk { get; set; }
        public Receta Receta { get; set; }
        public int IdMedicamentoFk { get; set; }
        public Medicamento Medicamento { get; set; }

        public decimal PrecioVenta {get; set;}
    }
