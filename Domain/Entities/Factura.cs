using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Factura:BaseEntity
    {
        public DateTime FechaCreacion { get; set; }


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdEmpleadoFk { get; set; }
        public int IdPacienteFk { get; set; }
        public int IdEstadoFk { get; set; }

        public Empleado Empleado { get; set; }
        public Paciente Paciente { get; set; }
        public Estado Estado { get; set; }

        
        /*
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A FACTURA (TABLAS QUE TIENEN DE FK AL ID DE FACTURA).
        */ 
        public ICollection<DetalleFactura> DetallesFacturas { get; set; }
        
    }
