using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class CompraProveedorDto:BaseEntity
    {
        public int IdProveedorFk { get; set; }
        public int IdMedicamentoFk {get; set;}
        public int IdEmpleadoFk { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public DateTime FechaCompra { get; set; }
    }
