using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Receta:BaseEntity
    {
        public DateTime FechaEmision {get; set;}
        public DateTime FechaVencimiento {get; set;}


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdDoctorFk {get; set;}
        public int IdPacienteFk {get; set;}
        
        public Doctor Doctor {get; set;}
        public Paciente Paciente {get; set;}

        /*
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A DIRECCION (TABLAS QUE TIENEN DE FK AL ID DE DIRECCION).
        */
        public ICollection<DetalleFactura> DetallesFacturas{get; set;}
    }
