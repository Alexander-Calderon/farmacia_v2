
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class EmpleadoDto:BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaRegistro {get; set;}
        public string Documento {get; set;}

        public int IdCargoFk {get; set;}
        public int IdTipoDocumentoFk {get; set;}
        public int IdDireccionFk {get; set;}
        public int IdUserFk {get; set;}

        public ICollection<ContactoEmpleado> ContactosEmpleados {get;set;}
    }
