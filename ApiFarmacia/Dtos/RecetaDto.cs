using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class RecetaDto:BaseEntity
    {
        public DateTime FechaVencimiento {get; set;}

        public int IdDoctorFk {get; set;}
    }
