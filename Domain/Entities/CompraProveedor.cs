using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class CompraProveedor:BaseEntity
    {
        public int IdProveedorFk { get; set; }
        public Proveedor Proveedor{ get; set; }

        public int IdMedicamentoFk {get; set;}
        public Medicamento Medicamento { get; set; }

        public int IdEmpleadoFk { get; set; }
        public Empleado Empleado { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public DateTime FechaCompra { get; set; }
    }
