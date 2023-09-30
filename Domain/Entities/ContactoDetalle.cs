using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class ContactoDetalle:BaseEntity
    {
        public string Descripcion {get; set;}

        public int IdContactoFk {get; set;}
        public Contacto Contacto   {get; set;}
        public int IdTipoContactoFk {get; set;}
        public TipoContacto TipoContacto {get; set;}    
    }
