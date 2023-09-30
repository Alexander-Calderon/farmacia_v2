using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Receta:BaseEntity
    {
        public DateTime FechaVencimiento {get; set;}

        public int IdDoctorFk {get; set;}
        public Doctor Doctor {get; set;}

        public ICollection<DetalleFactura> DetalleFacturas{get; set;}
    }
