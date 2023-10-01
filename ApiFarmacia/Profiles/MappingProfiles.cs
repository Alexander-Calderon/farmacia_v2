

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
        
    }
}
