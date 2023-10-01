using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Categoria:BaseEntity
    {
        public string Descripcion {get; set;}





        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A CATEGORIA (TABLAS QUE TIENEN DE FK AL ID DE CATEGORIA).            
        */
        public ICollection<Medicamento> Medicamentos {get; set;}
        
    }
