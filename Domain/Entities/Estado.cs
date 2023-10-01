using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Estado : BaseEntity
    {
        public string Nombre { get; set;}


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A ESTADO (TABLAS QUE TIENEN DE FK AL ID DE ESTADO).
        */ 
        public ICollection<Factura> Facturas { get; set;}
    }
