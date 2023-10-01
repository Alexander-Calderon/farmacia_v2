

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICargo Cargos { get; }
        ICategoria Categorias { get; }
        ICiudad Cuidades { get; }
        ICompraProveedor CompraProveedores { get; }
        IContacto Contactos { get; }
        IDepartamento Departamentos { get;}
        IDetalleFactura DetalleFacturas { get; }
        IDireccion Direcciones { get; }
        IDoctor Doctores { get; }
        IEmpleado Empleados { get; }
        IEspecializacion Especializacion { get; }
        IEstado Estados { get; }
        IFactura Facturas { get; }
        IMarca Marcas { get; }
        IMedicamento Medicamentos { get; }
        IPaciente Pacientes { get; }
        IPais Paises {get;}
        IProveedor Proveedores { get; }
        IReceta Recetas { get; }
        IRol Roles { get; }
        ITipoContacto TipoContactos { get; }
        ITipoDocumento TipoDocumentos { get; }
        ITipoPresentacion TipoPresentaciones { get; }

    }
}