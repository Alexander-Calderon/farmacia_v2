using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class ContactoDetalleDto:BaseEntity
    {
        public string Descripcion {get; set;}

        public int IdContactoFk {get; set;}
        public int IdTipoContactoFk {get; set;}
    }
