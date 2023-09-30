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

        public int IdCategoriaFk { get; set; }
        public Categoria Categoria { get; set; }

        public int IdTipoPresentacionFk { get; set; }
        public TipoPresentacion TipoPresentacion { get; set; }

        public int IdMarcaFk { get; set; }
        public Marca Marca { get; set; }

        public ICollection<DetalleFactura> DetalleFacturas { get; set; }
        
    }
