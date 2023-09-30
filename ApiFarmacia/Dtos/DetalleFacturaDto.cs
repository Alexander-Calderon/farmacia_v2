using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class DetalleFacturaDto:BaseEntity
    {
        public int IdFacturaFk { get; set; }
        public int IdRecetaFk { get; set; }
        public int IdMedicamentoFk { get; set; }
        public decimal PrecioVenta {get; set;}
    }
