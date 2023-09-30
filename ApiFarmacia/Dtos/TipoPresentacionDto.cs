using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class TipoPresentacionDto:BaseEntity
    {
        public string Descripcion {get; set;}
    }
