using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class DoctorDto:BaseEntity
    {
        public string Nombre {get; set;}
        public DateTime FechaRegistro {get; set;}
        public string Documento {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public int IdEspecialidadFk {get; set;}
    }
