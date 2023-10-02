

using ApiFarmacia.Dtos;
using Domain.Entities;
using AutoMapper;

namespace ApiFarmacia.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Cargo, CargoDto>().ReverseMap();
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Ciudad, CiudadDto>().ReverseMap();
        CreateMap<CompraProveedor, CompraProveedorDto>().ReverseMap();
        CreateMap<Contacto, ContactoDto>().ReverseMap();
        CreateMap<Departamento, DepartamentoDto>().ReverseMap();
        CreateMap<DetalleFactura, DetalleFacturaDto>().ReverseMap();
        CreateMap<Direccion, DireccionDto>().ReverseMap();
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Empleado, EmpleadoDto>().ReverseMap();
        CreateMap<Especializacion, EspecializacionDto>().ReverseMap();
        CreateMap<Estado, EmpleadoDto>().ReverseMap();
        CreateMap<Factura, FacturaDto>().ReverseMap();
        CreateMap<Marca, MarcaDto>().ReverseMap();
        CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
        CreateMap<Paciente, PacienteDto>().ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();
        CreateMap<Proveedor, ProveedorDto>().ReverseMap();
        CreateMap<Receta, RecetaDto>().ReverseMap();
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<TipoContacto, TipoContactoDto>().ReverseMap();
        CreateMap<TipoDocumento, TipoDocumentoDto>().ReverseMap();
        CreateMap<TipoPresentacion, TipoPresentacionDto>().ReverseMap();
        
    }
}
