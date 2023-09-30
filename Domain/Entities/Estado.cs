using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Estado
    {
        public string Nombre { get; set;}
        public ICollection<Factura> Facturas { get; set;}
    }
