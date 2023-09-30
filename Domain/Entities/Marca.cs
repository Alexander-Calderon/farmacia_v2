using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Marca:BaseEntity
    {
        public string Descripcion { get; set; }

        public ICollection<Medicamento> Medicamentos { get; set; }
    }
