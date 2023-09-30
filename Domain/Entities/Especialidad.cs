using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Especialidad:BaseEntity
    {
        public string Descripcion {get; set;}
        public ICollection<Doctor> Doctores {get; set;}
    }
