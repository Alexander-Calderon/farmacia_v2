using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class PaisDto:BaseEntity
    {
        public string  Nombre { get; set; }
    }
