using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Marca:BaseEntity
    {

        public string Nombre { get; set; }


        /*
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A MARCA (TABLAS QUE TIENEN DE FK AL ID DE MARCA).
        */ 
        public ICollection<Medicamento> Medicamentos { get; set; }
    }

