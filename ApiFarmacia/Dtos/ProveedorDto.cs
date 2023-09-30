using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class ProveedorDto:BaseEntity
    {
        public string Nombre {get; set;}
        public string Documento {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public int IdContactoFk {get; set;}
        public int IdDireccionFk {get; set;}
    }
