using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class Paciente:BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Documento { get; set; }
        public int IdTipoDocumentoFk { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public int IdDireccionFk { get; set; }
        public Direccion Direccion { get; set; }
        public ICollection<Factura> Facturas{ get; set; }

    }
