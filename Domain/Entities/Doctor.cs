using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Doctor:BaseEntity
    {
        public string Nombre {get; set;}
        public DateTime FechaRegistro {get; set;}
        public string Documento {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public TipoDocumento TipoDocumento {get; set;}
        public int IdEspecialidadFk {get; set;}
        public Especialidad Especialidad {get; set;}
        public ICollection<Receta> Recetas {get; set;}

    }
