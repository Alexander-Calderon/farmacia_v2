using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class User:BaseEntity
    {
        public string UserName {get; set;}
        public string Password {get; set;}


        /* 
            * FORÁNEAS.
        */
        public int IdRolFk {get; set;}

        // * #PROPIEDADES DE NAVEGABILIDAD#
        // OBJETOS DE QUE SERÁN USADOS EN RELACIONES EN !SENTIDO! DE UNO A UNO, COMO EL CASO DE LA FK EN SENTIDO HACIA LA PK EN RELACIONES DE UNO A MUCHOS O UNO A UNO.
        public Rol Rol {get; set;}

        public Empleado Empleado {get; set;} // ! Se añade para navegabilidad bidireccional, Relación de uno a uno


        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DIRECCION (TABLAS QUE TIENEN DE FK AL ID DE DIRECCION).
        */
        //// public ICollection<Empleado> Empleados {get; set;}
    }
