

namespace Domain.Entities;

    public class Ciudad: BaseEntity
    {
        public string Nombre { get; set; }


        /* 
            * FORÁNEAS Y OBJETOS DE LAS FORÁNEAS PARA QUE SE RECONOZCAN COMO FK Y SE CREE LA RELACIÓN.
        */
        public int IdDepartamentoFK {get; set;}

        public Departamento Departamento { get; set; }

        
        /* 
            * COLECCIONES DE DATOS PARA PODER ACCEDER A LA INFORMACIÓN DE LAS TABLAS QUE REFERENCIAN A CIUDAD (TABLAS QUE TIENEN DE FK AL ID DE CIUDAD).            
        */                
        public ICollection<Direccion> Direcciones { get; set;}
    }
