using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class MedicamentoDto:BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public int IdCategoriaFk { get; set; }
        public int IdTipoPresentacionFk { get; set; }
        public int IdMarcaFk { get; set; }
    }
