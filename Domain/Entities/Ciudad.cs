

namespace Domain.Entities;

    public class Ciudad: BaseEntity
    {
        public string Nombre { get; set; }

        public int IdDepartamentoFK {get; set;}
        public Departamento Departamento { get; set; }
        
        public ICollection<Direccion> Direcciones { get; set;}
    }
