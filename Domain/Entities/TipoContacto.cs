using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class TipoContacto:BaseEntity
    {
        public string Descripcion {get; set;}
        public ICollection<ContactoDetalle> ContactoDetalles {get; set;}
    }
