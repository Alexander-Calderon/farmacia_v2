using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Ciudad: BaseEntity
    {
        public string Nombre { get; set; }

        public int IdDepartamento {get; set;}
        public Departamento Departamento { get; set; }
    }
